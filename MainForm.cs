using System;
using System.Collections;
using System.Windows.Forms;
using Tekla.Structures;
using Tekla.Structures.Dialog;
using Tekla.Structures.Dialog.UIControls;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using K_BoltBrace;
using System.Collections.Generic;
using System.Threading;

namespace K_PlateOnAngleBeam
{
    public partial class MainForm : PluginFormBase
    {
        string _Boltsize = "";
        string _Boltstandard = "";
        string _Bolttole = "";
        string _Boltextra = "";
        private Events _eventmodelchange = new Events();


        public void DangkyEvent()
        {
            _eventmodelchange.ModelObjectChanged += _eventmodelchange_ModelObjectChanged;
            _eventmodelchange.Register();
        }

        public void HuydangkyEvent()
        {
            _eventmodelchange.UnRegister();
        }

        private void _eventmodelchange_ModelObjectChanged(List<ChangeData> Changes)
        {
            foreach (ChangeData item in Changes)
            {
                ChangeData.ChangeTypeEnum tr = ChangeData.ChangeTypeEnum.OBJECT_INSERT;

                if (item.Type == tr)
                {
                    BoltGroup b = (BoltGroup)item.Object;
                    ArrayList ObjectsToSelect = new ArrayList();
                    ObjectsToSelect.Add(b);
                    Tekla.Structures.Model.UI.ModelObjectSelector MS = new Tekla.Structures.Model.UI.ModelObjectSelector();
                    MS.Select(ObjectsToSelect);
                    new Tekla.Structures.Model.Model().CommitChanges();
                    ModelObjectEnumerator obj = MS.GetSelectedObjects();

                    while (obj.MoveNext())
                    {
                        BoltGroup bb = obj.Current as BoltGroup;
                        _Boltsize = bb.BoltSize.ToString();
                        _Boltstandard = bb.BoltStandard;
                        _Bolttole = bb.Tolerance.ToString();
                        _Boltextra = bb.ExtraLength.ToString();
                    }


                }



            }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        protected override string LoadValuesPath(string FileName)
        {
            SetAttributeValue(textBox1, "100");
            SetAttributeValue(textBox2, "100");

            SetAttributeValue(txt_paedit, "P");
            SetAttributeValue(txt_panumedit, "1");
            SetAttributeValue(txt_aseedit, "A");
            SetAttributeValue(txt_asenumedit, "1");
            SetAttributeValue(txt_coloredit, "2");
            SetAttributeValue(txt_fiedit, "");
            SetAttributeValue(txt_naedit, "BentPlate");
            SetAttributeValue(txt_maedit, "SS400");

            SetAttributeValue(txt_thickness, "5");

            SetAttributeValue(txt_bolttole, "2");
            SetAttributeValue(txt_boltstan, "8.8XOX");
            SetAttributeValue(txt_boltsize, "12");
            SetAttributeValue(ck_bolt, "1");
            SetAttributeValue(ck_nut1, "1");
            SetAttributeValue(ck_washer2, "1");
            SetAttributeValue(ck_washer3, "1");



            this.Apply();
            return base.LoadValuesPath(FileName);
        }
        private void OkApplyModifyGetOnOffCancel_OkClicked(object sender, EventArgs e)
        {
            this.Apply();
            HuydangkyEvent();
            this.Close();
        }

        private void OkApplyModifyGetOnOffCancel_ApplyClicked(object sender, EventArgs e)
        {
            this.Apply();
        }

        private void OkApplyModifyGetOnOffCancel_ModifyClicked(object sender, EventArgs e)
        {

            this.Modify();
        }

        private void OkApplyModifyGetOnOffCancel_GetClicked(object sender, EventArgs e)
        {
            this.Get();
        }

        private void OkApplyModifyGetOnOffCancel_OnOffClicked(object sender, EventArgs e)
        {
            this.ToggleSelection();
        }

        private void OkApplyModifyGetOnOffCancel_CancelClicked(object sender, EventArgs e)
        {
            HuydangkyEvent();
            this.Close();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DangkyEvent();
        }



        private void materialCatalog_SelectionDone(object sender, EventArgs e)
        {
            SetAttributeValue(txt_maedit, materialCatalog.SelectedMaterial);
        }



        private void txt_Bolt_tole_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            FormBolt form = new FormBolt();
            form.ShowDialog();
            //if (_Boltsize == "")
            //{
            //    this.Modify();
            //    Thread.Sleep(100);

            //}
            //else
            //{
            //    Thread.Sleep(100);
            //}


            //AddForm();
        }

        private void FormBolt_Load(object sender, EventArgs e)
        {

        }

        public void AddForm()
        {

            PluginFormBase fr = new PluginFormBase();
            CheckBox ch_checkw1 = new CheckBox();
            CheckBox ch_checkw3 = new CheckBox();
            CheckBox ch_checkw2 = new CheckBox();
            CheckBox ch_checkn1 = new CheckBox();
            CheckBox ch_checkn2 = new CheckBox();
            CheckBox ch_checkb = new CheckBox();
            Label Lb_Tolerance = new Label();
            TextBox bolttole = new TextBox();
            BoltCatalogSize boltSize = new BoltCatalogSize();
            BoltCatalogStandard boltstandard = new BoltCatalogStandard();

            TextBox boltextra = new TextBox();
            Label Lb_Boltstandard = new Label();
            Label Lb_Boltsize = new Label();
            Button btn_ok = new Button();


            ch_checkw1.Location = new System.Drawing.Point(194, 150);
            ch_checkw1.Margin = new System.Windows.Forms.Padding(4);
            ch_checkw1.Size = new System.Drawing.Size(79, 21);
            ch_checkw1.Text = "Washer";
            ch_checkw1.UseVisualStyleBackColor = true;

            // 
            // ch_checkw3
            // 

            ch_checkw3.Location = new System.Drawing.Point(194, 223);
            ch_checkw3.Margin = new System.Windows.Forms.Padding(4);
            ch_checkw3.Size = new System.Drawing.Size(79, 21);
            ch_checkw3.Text = "Washer";
            ch_checkw3.UseVisualStyleBackColor = true;
            // 
            // ch_checkw2
            // 

            ch_checkw2.Checked = true;
            ch_checkw2.CheckState = System.Windows.Forms.CheckState.Checked;
            ch_checkw2.Location = new System.Drawing.Point(194, 197);
            ch_checkw2.Margin = new System.Windows.Forms.Padding(4);
            ch_checkw2.Size = new System.Drawing.Size(79, 21);
            ch_checkw2.Text = "Washer";
            ch_checkw2.UseVisualStyleBackColor = true;
            // 
            // ch_checkn1
            // 

            ch_checkn1.Checked = true;
            ch_checkn1.CheckState = System.Windows.Forms.CheckState.Checked;
            ch_checkn1.Location = new System.Drawing.Point(194, 247);
            ch_checkn1.Margin = new System.Windows.Forms.Padding(4);
            ch_checkn1.Size = new System.Drawing.Size(52, 21);
            ch_checkn1.Text = "Nut";
            ch_checkn1.UseVisualStyleBackColor = true;
            // 
            // ch_checkn2
            // 

            ch_checkn2.Checked = true;
            ch_checkn2.CheckState = System.Windows.Forms.CheckState.Checked;
            ch_checkn2.Location = new System.Drawing.Point(194, 273);
            ch_checkn2.Margin = new System.Windows.Forms.Padding(4);
            ch_checkn2.Size = new System.Drawing.Size(52, 21);
            ch_checkn2.Text = "Nut";
            ch_checkn2.UseVisualStyleBackColor = true;
            // 
            // ch_checkb
            // 

            ch_checkb.Checked = true;
            ch_checkb.CheckState = System.Windows.Forms.CheckState.Checked;
            ch_checkb.Location = new System.Drawing.Point(194, 125);
            ch_checkb.Margin = new System.Windows.Forms.Padding(4);
            ch_checkb.Size = new System.Drawing.Size(54, 21);
            ch_checkb.Text = "Bolt";
            ch_checkb.UseVisualStyleBackColor = true;
            // 
            // label17
            // 

            Lb_Tolerance.Location = new System.Drawing.Point(30, 93);
            Lb_Tolerance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Lb_Tolerance.Size = new System.Drawing.Size(72, 17);
            Lb_Tolerance.Text = "Tolerance";
            // 
            // txt_Bolt_tole
            // 

            bolttole.Location = new System.Drawing.Point(149, 89);
            bolttole.Margin = new System.Windows.Forms.Padding(4);
            bolttole.Size = new System.Drawing.Size(160, 22);

            // 
            // boltSize
            // 

            boltSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            boltSize.FormattingEnabled = true;
            boltSize.Location = new System.Drawing.Point(149, 56);
            boltSize.Size = new System.Drawing.Size(160, 24);

            // 
            // txt_Bolt_extra
            // 

            boltextra.Location = new System.Drawing.Point(62, 295);
            boltextra.Margin = new System.Windows.Forms.Padding(4);
            boltextra.Size = new System.Drawing.Size(96, 22);

            // 
            // boltstandard
            // 

            boltstandard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            boltstandard.FormattingEnabled = true;
            boltstandard.LinkedBoltCatalogSize = boltSize;
            boltstandard.Location = new System.Drawing.Point(149, 19);
            // boltstandard.Margin = new System.Windows.Forms.Padding(4);
            boltstandard.Size = new System.Drawing.Size(160, 24);


            // 
            // label11
            // 

            Lb_Boltstandard.Location = new System.Drawing.Point(30, 23);
            Lb_Boltstandard.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Lb_Boltstandard.Size = new System.Drawing.Size(92, 17);
            Lb_Boltstandard.Text = "Bolt standard";
            // 
            // label3
            // 


            Lb_Boltsize.Location = new System.Drawing.Point(30, 56);
            Lb_Boltsize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Lb_Boltsize.Size = new System.Drawing.Size(61, 17);
            Lb_Boltsize.Text = "Bolt size";



            btn_ok.Location = new System.Drawing.Point(209, 328);
            btn_ok.Size = new System.Drawing.Size(117, 43);
            btn_ok.Text = "Apply";
            btn_ok.UseVisualStyleBackColor = true;


            PictureBox pic1 = new PictureBox();

            fr.Controls.Add(btn_ok);
            fr.Controls.Add(ch_checkw1);
            fr.Controls.Add(ch_checkw3);
            fr.Controls.Add(ch_checkw2);
            fr.Controls.Add(ch_checkn1);
            fr.Controls.Add(ch_checkn2);
            fr.Controls.Add(ch_checkb);
            fr.Controls.Add(Lb_Tolerance);
            fr.Controls.Add(bolttole);
            fr.Controls.Add(boltSize);
            fr.Controls.Add(boltextra);
            fr.Controls.Add(boltstandard);
            fr.Controls.Add(Lb_Boltstandard);
            fr.Controls.Add(Lb_Boltsize);

            fr.Size = new System.Drawing.Size(543, 444);
            fr.TopMost = true;
            fr.MaximizeBox = false;
            fr.MinimizeBox = false;
            fr.MaximumSize = new System.Drawing.Size(543, 444);
            fr.MinimumSize = new System.Drawing.Size(543, 444);
            //  fr.StartPosition = FormStartPosition.CenterParent;
            fr.Text = "Form Bolt";



            fr.Show();


            K_CallBack.SetText(fr, boltstandard, _Boltstandard);
            K_CallBack.SetText(fr, boltSize, _Boltsize + ".00");
            K_CallBack.SetText(fr, bolttole, _Bolttole);
            K_CallBack.SetText(fr, boltextra, _Boltextra);




            //  MessageBox.Show(_Boltstandard + "-" + _Boltsize);

            btn_ok.Click += Btn_ok_Click;

        }



        private void Btn_ok_Click(object sender, EventArgs e)
        {
            K_PlateOnAngleBeam.databolt.Bolt_size = 30;
            this.Modify();

        }




        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            HuydangkyEvent();
        }
    }




}

