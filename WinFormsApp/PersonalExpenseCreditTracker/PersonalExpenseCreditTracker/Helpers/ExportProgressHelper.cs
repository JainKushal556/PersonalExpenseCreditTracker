using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PersonalExpenseCreditTracker.Helpers
{
    /// <summary>
    /// সব মডিউলে Excel Export এ ব্যবহারযোগ্য প্রগ্রেস বার ওভারলে হেল্পার।
    /// Export শুরু হলে ফুল‑স্ক্রিন ব্যাকড্রপ + প্রগ্রেস বার দেখায়,
    /// ১০০% হলে OK বাটন দেখায়, OK ক্লিক করলে ওভারলে সরে যায়।
    /// </summary>
    public class ExportProgressHelper
    {
        private Panel pnlCard;
        private EventHandler parentResizeHandler;
        private Label lblTitle;
        private Label lblPercent;
        private Panel pnlProgressTrack;
        private Panel pnlProgressFill;
        private Button btnOk;
        private Timer tmrProgress;
        private int currentProgress = 0;
        private int targetProgress = 0;
        private Control parentControl;
        private Action onComplete;
        private BlockInputMessageFilter messageFilter;

        private class BlockInputMessageFilter : IMessageFilter
        {
            private Control allowedControl;
            public BlockInputMessageFilter(Control allowed) { this.allowedControl = allowed; }
            public bool PreFilterMessage(ref Message m)
            {
                if ((m.Msg >= 0x0200 && m.Msg <= 0x020E) || (m.Msg >= 0x0100 && m.Msg <= 0x0109))
                {
                    Control target = Control.FromHandle(m.HWnd);
                    if (target != null)
                    {
                        Control current = target;
                        while (current != null)
                        {
                            if (current == allowedControl) return false;
                            current = current.Parent;
                        }
                    }
                    return true; 
                }
                return false;
            }
        }

        [DllImport("gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect,
            int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        private void SetRadius(Control control, int radius)
        {
            if (control == null || control.Width <= 0 || control.Height <= 0) return;
            IntPtr hrgn = CreateRoundRectRgn(0, 0, control.Width + 1, control.Height + 1, radius, radius);
            Region region = Region.FromHrgn(hrgn);
            if (control.Region != null) control.Region.Dispose();
            control.Region = region;
            DeleteObject(hrgn);
        }

        /// <summary>
        /// Export প্রগ্রেস ওভারলে শুরু করে।
        /// </summary>
        /// <param name="parent">যে Form বা Control এর উপর ওভারলে দেখাবে</param>
        /// <param name="title">যেমন "Exporting Expense Data..."</param>
        /// <param name="completeCallback">OK বাটনে ক্লিক করলে এটি কল হবে (nullable)</param>
        public void Show(Control parent, string title = "Exporting File...", Action completeCallback = null)
        {
            parentControl = parent;
            onComplete = completeCallback;
            currentProgress = 0;
            targetProgress = 0;

            // ======== ২. সেন্টার কার্ড (Professional White Card) ========
            pnlCard = new Panel
            {
                Size = new Size(450, 180),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // ---- Title Label ----
            lblTitle = new Label
            {
                Text = title,
                ForeColor = Color.FromArgb(30, 41, 59),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(25, 25)
            };

            // ---- Percent Label (Status Text) ----
            lblPercent = new Label
            {
                Text = "Preparing to export data...",
                ForeColor = Color.DimGray,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(25, 65)
            };

            // ---- Progress Bar Track ----
            pnlProgressTrack = new Panel
            {
                Location = new Point(25, 95),
                Size = new Size(400, 10),
                BackColor = Color.FromArgb(226, 232, 240)
            };

            // ---- Progress Bar Fill ----
            pnlProgressFill = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(0, 10),
                BackColor = Color.FromArgb(59, 130, 246)
            };

            pnlProgressTrack.Controls.Add(pnlProgressFill);

            // ---- OK Button (Bottom Right) ----
            btnOk = new Button
            {
                Text = "OK",
                ForeColor = Color.White,
                BackColor = Color.FromArgb(59, 130, 246),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Size = new Size(90, 32),
                Location = new Point(335, 125),
                Cursor = Cursors.Hand,
                Visible = false
            };
            btnOk.FlatAppearance.BorderSize = 0;
            btnOk.Click += BtnOk_Click;

            pnlCard.Controls.Add(lblTitle);
            pnlCard.Controls.Add(lblPercent);
            pnlCard.Controls.Add(pnlProgressTrack);
            pnlCard.Controls.Add(btnOk);

            // ---- রিসাইজ হলে কার্ড সবসময় সেন্টারে ----
            parentResizeHandler = (s, e) => CenterCard();
            parent.Resize += parentResizeHandler;

            parent.Controls.Add(pnlCard);
            pnlCard.BringToFront();
            CenterCard();
            
            if (messageFilter != null) Application.RemoveMessageFilter(messageFilter);
            messageFilter = new BlockInputMessageFilter(pnlCard);
            Application.AddMessageFilter(messageFilter);
            
            // Set radius for elements (No radius for the main card as we used BorderStyle.FixedSingle for a crisp desktop look)
            SetRadius(pnlProgressTrack, 5);
            SetRadius(pnlProgressFill, 5);
            SetRadius(btnOk, 4);

            Application.DoEvents();
        }

        private void CenterCard()
        {
            if (pnlCard != null && parentControl != null)
            {
                pnlCard.Location = new Point(
                    (parentControl.Width - pnlCard.Width) / 2,
                    (parentControl.Height - pnlCard.Height) / 2
                );
            }
        }

        /// <summary>
        /// প্রগ্রেস স্মুথভাবে একটু একটু করে বাড়ায় (০-১০০)।
        /// এক্সপোর্ট শুরু হওয়ার আগে কল করুন SetProgress(10),
        /// ডেটা লেখা শেষে SetProgress(90), সেভ শেষে SetProgress(100)।
        /// </summary>
        public void SetProgress(int percent)
        {
            if (percent < 0) percent = 0;
            if (percent > 100) percent = 100;
            targetProgress = percent;

            if (tmrProgress == null)
            {
                tmrProgress = new Timer();
                tmrProgress.Interval = 20;
                tmrProgress.Tick += TmrProgress_Tick;
            }
            tmrProgress.Start();
        }

        private void TmrProgress_Tick(object sender, EventArgs e)
        {
            if (currentProgress < targetProgress)
            {
                currentProgress += 2;
                if (currentProgress > targetProgress)
                    currentProgress = targetProgress;

                UpdateProgressUI();
            }
            else
            {
                tmrProgress.Stop();

                if (currentProgress >= 100)
                {
                    OnExportComplete();
                }
            }
        }

        private void UpdateProgressUI()
        {
            if (pnlProgressFill == null || pnlProgressTrack == null || lblPercent == null) return;

            int fillWidth = (int)((currentProgress / 100.0) * pnlProgressTrack.Width);
            pnlProgressFill.Size = new Size(fillWidth, pnlProgressTrack.Height);
            SetRadius(pnlProgressFill, 5);
            lblPercent.Text = "Exporting data... " + currentProgress + "%";
            Application.DoEvents();
        }

        private void OnExportComplete()
        {
            if (lblTitle != null)
            {
                lblTitle.Text = "Export Successful";
                lblTitle.ForeColor = Color.FromArgb(34, 197, 94); // Green
            }
            if (lblPercent != null)
            {
                lblPercent.Text = "100% — File saved successfully.";
                lblPercent.ForeColor = Color.DimGray;
            }
            if (pnlProgressFill != null)
            {
                pnlProgressFill.BackColor = Color.FromArgb(34, 197, 94); // Green
            }
            if (btnOk != null)
            {
                btnOk.Visible = true;
            }
            Application.DoEvents();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            Close();
            if (onComplete != null)
            {
                onComplete();
            }
        }

        /// <summary>
        /// ওভারলে বন্ধ করে এবং সব রিসোর্স পরিষ্কার করে।
        /// </summary>
        public void Close()
        {
            if (messageFilter != null)
            {
                Application.RemoveMessageFilter(messageFilter);
                messageFilter = null;
            }

            if (tmrProgress != null)
            {
                tmrProgress.Stop();
                tmrProgress.Dispose();
                tmrProgress = null;
            }

            if (pnlCard != null && parentControl != null)
            {
                if (parentResizeHandler != null)
                {
                    parentControl.Resize -= parentResizeHandler;
                    parentResizeHandler = null;
                }
                parentControl.Controls.Remove(pnlCard);
                pnlCard.Dispose();
                pnlCard = null;
            }

            parentControl = null;
        }
    }
}
