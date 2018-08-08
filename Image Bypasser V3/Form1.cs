using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_Bypasser_V3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //MessageBox.Show("Version V3 R2 Speed Improvements!");
            InitializeComponent();
        }
        
        private void monoFlat_Button1_Click(object sender, EventArgs e)
        {
            this.dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;...";
            this.dialog.Title = "Select a picture to convert.";
            bool flag = this.dialog.ShowDialog() == DialogResult.OK;
            if (flag)
            {
                this.pictureBox1.ImageLocation = this.dialog.FileName;
            }
        }
        private string str = "";
        private void monoFlat_Button2_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            this.str = "spawn(function() local imageBytes = {";
            this.pictureBox1.BackColor = Color.Transparent;
            Bitmap bitmap = new Bitmap(this.dialog.FileName);
            int num = int.Parse(this.textBox2.Text);
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    bool flag = i % num == 0 && j % num == 0;
                    if (flag)
                    {
                        Color pixel = bitmap.GetPixel(i, j);
                        this.str = string.Concat(new object[]
                        {
                            this.str,
                            "\n {x = ",
                            i,
                            ", y = ",
                            j,
                            ", r = ",
                            pixel.R,
                            ", g = ",
                            pixel.G,
                            " , b = ",
                            pixel.B,
                            "}, "
                        });
                    }
                }
            }
            this.str = string.Concat(new object[]
            {
                this.str,
                "}\r\nlocal X = ",
                this.pictureBox1.Width,
                " local Y = ",
                this.pictureBox1.Height
            });
            this.str += " local P = Instance.new('Part')\r\nP.Name = 'Bitmap'\r\nP.Anchored = true\r\nP.Transparency = 1\r\nP.Position = Vector3.new(0, 0, 0)\r\nP.Parent = game.Workspace\r\n\r\nlocal B = Instance.new('BillboardGui')\r\nB.Name = 'Image'\r\nB.AlwaysOnTop = true\r\nB.Size = UDim2.new(0, X, 0, Y)\r\nB.Parent = P\r\n local F = Instance.new('Frame')\r\nF.Name = 'Holder'\r\nF.Size = UDim2.new(1, 0, 1, 0)\r\nF.BackgroundTransparency = 1\r\nF.Parent = B";
            this.str = this.str + "\r\nlocal res = " + this.textBox2.Text;
            this.str += " local current = 1\r\n\r\nlocal cooldown = false\r\n\r\nlocal cooldowncount = 0\r\n\r\nfor _,v in pairs(imageBytes) do\r\n\t spawn(function() local frame = Instance.new('Frame')\r\n\tframe.BackgroundColor3 = Color3.new(v.r/255, v.g/255, v.b/255)\r\n\tframe.Size = UDim2.new(0, 1, 0, 1)\r\n\tframe.BorderSizePixel = 0\r\n\tframe.Position = UDim2.new(0, math.floor(v.x/res), 0, math.floor(v.y/res))\r\n\tframe.Parent = F\r\n\tif frame.BackgroundColor3 == Color3.new(0, 0, 0) then\r\n\t\tframe:Destroy()\r\n\tend end)\r\n\tend end)";
            this.textBox1.Text = this.str;
        }

        private void monoFlat_Button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            // set a default file name
            savefile.FileName = "decal.lua";
            // set filters - this can be done in properties as well
            savefile.Filter = "Lua files (*.lua)|*.lua|All files (*.*)|*.*";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(savefile.FileName))
                    sw.Write(textBox1.Text);
            }

        }

        private void monoFlat_Button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }

        private void monoFlat_ThemeContainer1_Click(object sender, EventArgs e)
        {

        }
    }
}
