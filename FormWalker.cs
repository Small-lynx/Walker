using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Walker
{
    public partial class FormWalker : Form
    {
        private BufferedGraphics buffered;
        private List<Bitmap> walls;
        private Bitmap background;
        private Labyrinth labyrinth;

        public FormWalker()
        {
            InitializeComponent();
            buffered = BufferedGraphicsManager.Current.Allocate(CreateGraphics(), DisplayRectangle);
            walls = new List<Bitmap>
            {
                new Bitmap(Properties.Resources.Block1, DisplayRectangle.Size),
                new Bitmap(Properties.Resources.Block2, DisplayRectangle.Size),
                new Bitmap(Properties.Resources.Block3, DisplayRectangle.Size),
                new Bitmap(Properties.Resources.Block4, DisplayRectangle.Size),
                new Bitmap(Properties.Resources.Block5, DisplayRectangle.Size),
                new Bitmap(Properties.Resources.Block6, DisplayRectangle.Size),
                new Bitmap(Properties.Resources.Block7, DisplayRectangle.Size),
                new Bitmap(Properties.Resources.Block8, DisplayRectangle.Size),
                new Bitmap(Properties.Resources.Block9, DisplayRectangle.Size),
                new Bitmap(Properties.Resources.Block10, DisplayRectangle.Size),
                new Bitmap(Properties.Resources.Block12, DisplayRectangle.Size),
            };
            background = new Bitmap(Properties.Resources.Background, DisplayRectangle.Size);
        }

        /// <summary>
        /// Перерисовка лабиринта
        /// </summary>
        private void RedrawLabirint(List<int> section)
        {
            buffered.Graphics.DrawImage(background, DisplayRectangle);
            for (var i = 0; i < walls.Count; i++)
            {
                if (section[i] == 1)
                {
                    if (i != walls.Count - 1)
                    {
                        if (i == 1 || i == 4 || i == 7)
                        {
                            buffered.Graphics.DrawImage(walls[i + 1], 0, 0);
                            buffered.Graphics.DrawImage(walls[i], 0, 0);
                            i++;
                        }
                        else
                        {
                            buffered.Graphics.DrawImage(walls[i], 0, 0);
                        }
                    }
                }
                else
                {
                    if (i == walls.Count - 1)
                    {
                        if (section[i + 1] == 1)
                        {
                            buffered.Graphics.DrawImage(walls[i], 0, 0);
                        }
                    }
                }
            }
            buffered.Render();
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        private void FormWalker_Load(object sender, EventArgs e)
        {
            int width = 11, height = 11;
            labyrinth = new Labyrinth(width, height);
            RedrawLabirint(labyrinth.Walking());
        }

        /// <summary>
        /// Нажатие клавиши
        /// </summary>
        private void FormWalker_KeyDown(object sender, KeyEventArgs e)
        {
            RedrawLabirint(labyrinth.Walking(e.KeyValue));
        }
    }
}
