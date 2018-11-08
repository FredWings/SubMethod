using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WndFormEventLoadOnce
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            KeyPress(txtOnce);
            txtMore.KeyPress += control_KeyPress;
            //GetEventKey();
        }

        /// <summary>
        /// 如果已经存在同名事件，则不加载
        /// </summary>
        /// <param name="box"></param>
        private void KeyPress(TextBox box)
        {
            PropertyInfo pi = box.GetType().GetProperty("Events", BindingFlags.Instance | BindingFlags.NonPublic);
            EventHandlerList ehl = (EventHandlerList)pi.GetValue(box, null);
            FieldInfo fieldInfo = (typeof(Control)).GetField("EventKeyPress", BindingFlags.Static | BindingFlags.NonPublic);
            Delegate d = ehl[fieldInfo.GetValue(null)];
            if ((d == null) || (!d.GetInvocationList().Contains((KeyPressEventHandler)control_KeyPress)))
            {
                box.KeyPress += new KeyPressEventHandler(control_KeyPress);
            }
        }

        private void control_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox box = sender as TextBox;

            if (box.Name == "txtOnce")
            {
                showOnce.Text += e.KeyChar.ToString();
            }
            else
            {
                showMore.Text += e.KeyChar.ToString();
            }
        }

        /// <summary>
        /// 获取事件的名称
        /// </summary>
        private void GetEventKey()
        {
            FieldInfo[] fieldInfos = (typeof(Control)).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            foreach(FieldInfo f in fieldInfos)
            {
                Debug.WriteLine(f.Name);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            showMore.Clear();
            showOnce.Clear();
        }
    }
}
