using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Dialog;
using Tekla.Structures.Plugins;

namespace K_PlateOnAngleBeam
{
    class K_CallBack
    {
         delegate void SetTextCallBack(PluginFormBase fr, Control ctr, string text);
         delegate void SetCheckBoxCallBack(PluginFormBase fr, CheckBox ctr, bool boo);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fr">PluginFormBase truyền vào</param>
        /// <param name="ctr">Control truyền vào</param>
        /// <param name="text">Dữ liệu text truyền vào</param>
        public static void SetText(PluginFormBase fr, Control ctr, string text)
        {
            if (ctr.InvokeRequired)
            {
                SetTextCallBack d = new SetTextCallBack(SetText);
                fr.Invoke(d, new object[] { fr, ctr, text });
            }
            else
            {
                ctr.Text = text;


            }
        }

        public static void SetBool(PluginFormBase fr, CheckBox ctr, bool boo)
        {
            if (ctr.InvokeRequired)
            {
                SetCheckBoxCallBack d = new SetCheckBoxCallBack(SetBool);
                fr.Invoke(d, new object[] { fr, ctr, boo });
            }
            else
            {
                ctr.Checked = boo;


            }
        }
    }
}
