using System.Threading.Tasks.Sources;
using System.Runtime.InteropServices;
using KRegedit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Tekla.Structures;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Plugins;
using K_BoltBrace;



namespace K_PlateOnAngleBeam
{
    public class PluginData
    {

        [StructuresField("typebolt")]
        public int typebolt;

        [StructuresField("dy")]
        public double dy;

        [StructuresField("gap")]
        public double gap;

        [StructuresField("fx")]
        public double fx;

        [StructuresField("fy")]
        public double fy;

        [StructuresField("w")]
        public double w;

        [StructuresField("h")]
        public double h;

        [StructuresField("thickness")]
        public double thickness;

        [StructuresField("b1x")]
        public string b1x;

        [StructuresField("b1y")]
        public string b1y;

        [StructuresField("bx")]
        public string bx;

        [StructuresField("by")]
        public string by;

        //Danh so

        [StructuresField("coloredit")]
        public string coloredit;

        [StructuresField("paedit")]
        public string paedit;

        [StructuresField("panumedit")]
        public int panumedit;

        [StructuresField("aseedit")]
        public string aseedit;

        [StructuresField("asenumedit")]
        public int asenumedit;

        [StructuresField("naedit")]
        public string naedit;

        [StructuresField("mnaedit")]
        public string maedit;

        [StructuresField("fiedit")]
        public string fiedit;

        //Bolt1


        ///Bolt


        [StructuresField("Bolt_type")]
        public string Bolt_type;

        [StructuresField("Bolt_size")]
        public double Bolt_size;

        [StructuresField("Bolt_extra")]
        public double Bolt_extra;

        [StructuresField("Bolt_tole")]
        public double Bolt_tole;

        [StructuresField("facebolt")]
        public int facebolt;

        [StructuresField("checkb")]
        public int checkb;

        [StructuresField("checkw1")]
        public int checkw1;

        [StructuresField("checkw2")]
        public int checkw2;

        [StructuresField("checkw3")]
        public int checkw3;

        [StructuresField("checkn1")]
        public int checkn1;

        [StructuresField("checkn2")]
        public int checkn2;



    }

    [Plugin("K_Plate_On_AngleBeam")]
    [PluginUserInterface("K_PlateOnAngleBeam.MainForm")]


    public class K_PlateOnAngleBeam : PluginBase
    {
        #region Fields
        private Model _Model;
        private PluginData _Data;
        KhuongVo kh = new KhuongVo();

        public static K_BoltBrace.PluginData databolt = new K_BoltBrace.PluginData();
        #endregion

        #region Properties
        private Model Model
        {
            get { return this._Model; }
            set { this._Model = value; }
        }

        private PluginData Data
        {
            get { return this._Data; }
            set { this._Data = value; }

        }






        #endregion

        #region Constructor
        public K_PlateOnAngleBeam(PluginData data)
        {
            Model = new Model();
            Data = data;


        }


        #endregion

        #region Overrides
        public override List<InputDefinition> DefineInput()
        {

            List<InputDefinition> PointList = new List<InputDefinition>();
            Picker Picker = new Picker();

            Part part1 = Picker.PickObject(Picker.PickObjectEnum.PICK_ONE_PART) as Part;

            PointList.Add(new InputDefinition(part1.Identifier));

            return PointList;
        }

        public override bool Run(List<InputDefinition> Input)
        {

            try
            {


                GetValuesFromDialog();
                WorkPlaneHandler wph = Model.GetWorkPlaneHandler();
                TransformationPlane modelplane = wph.GetCurrentTransformationPlane();

                Identifier iden1 = (Identifier)Input[0].GetInput();

                Beam beam1 = Model.SelectModelObject(iden1) as Beam;

                Point pointLength1 = beam1.GetCenterLine(true)[0] as Point;
                Point pointLength2 = beam1.GetCenterLine(true)[1] as Point;
                double LengthBeam = Distance.PointToPoint(pointLength1, pointLength2);
                CoordinateSystem coor1 = kh.getCsRotate(beam1.GetCoordinateSystem(), -90, 0, 0);

                Point p = GetCenterPointAngleSection(beam1);

                Point p1 = kh.PointPlus(p, coor1.AxisX, -Data.w / 2);
                Point p2 = kh.PointPlus(p, coor1.AxisX, Data.w / 2);

                p1 = kh.PointPlus(p1, coor1.AxisY, Data.gap);
                p2 = kh.PointPlus(p2, coor1.AxisY, Data.gap);

                Point p3 = kh.PointPlus(p2, coor1.AxisY, Data.h);
                Point p4 = kh.PointPlus(p1, coor1.AxisY, Data.h);

                Chamfer ch1 = new Chamfer(Data.fx, Data.fy, Chamfer.ChamferTypeEnum.CHAMFER_LINE);
                Chamfer ch2 = new Chamfer(Data.fy, Data.fx, Chamfer.ChamferTypeEnum.CHAMFER_LINE);


                ContourPlate cPlate = new ContourPlate();
                cPlate.AddContourPoint(new ContourPoint(p1, null));
                cPlate.AddContourPoint(new ContourPoint(p2, null));
                cPlate.AddContourPoint(new ContourPoint(p3, ch2));
                cPlate.AddContourPoint(new ContourPoint(p4, ch1));
                string prolife = "PL" + Convert.ToString(Data.thickness);
                cPlate.Profile.ProfileString = prolife;
                cPlate.Material.MaterialString = "S275";
                cPlate.Class = "2";
                cPlate.Position.Depth = Position.DepthEnum.FRONT;

                cPlate.Insert();
                Danhso(cPlate);

                List<double> LitsbX = new List<double>();
                if (Data.bx != "")
                {
                    LitsbX = kh.GetListDoubleFormDistanceTextBox(Data.bx);
                }
                double totalbX = kh.TotalListDouble(LitsbX);

               
                databolt.Bolt_extra = 0;
                databolt.Bolt_size = 12;
                databolt.Bolt_tole = 2;
                databolt.Bolt_type = "8.8XOX";
                databolt.Bolt_x = Data.bx;
                databolt.Bolt_y = Data.by;
                databolt.bracecut = 0;
                databolt.checkb = 1;
                databolt.checkn1 = 1;
                databolt.checkn2 = 0;
                databolt.checkw1 = 0;
                databolt.checkw2 = 1;
                databolt.checkw3 = 0;
                databolt.facebolt = 0;
                databolt.Dx = Data.w / 2 - (totalbX / 2);
                databolt.Dy = -Data.dy;
                databolt.isCut = 0;

                int kieuraibolt = 0;
                if (Data.typebolt.ToString() == "1")
                {
                    kieuraibolt = 1;
                }
                if (Data.typebolt.ToString() == "2")
                {
                    kieuraibolt = 2;
                }

                databolt.typebolt = kieuraibolt;
                K_BoltBrace.K_BoltBrace bolt = new K_BoltBrace.K_BoltBrace(databolt);
                List<InputDefinition> listInput = new List<InputDefinition>();
                listInput.Add(new InputDefinition(beam1.Identifier));
                listInput.Add(new InputDefinition(cPlate.Identifier));
                bolt.Run(listInput);
                Model.CommitChanges();

                List<BoltGroup> ListBolt = new List<BoltGroup>();
                ModelObjectEnumerator objs = cPlate.GetBolts();
                while (objs.MoveNext())
                {
                    BoltGroup b = objs.Current as BoltGroup;
                    if (b != null)
                    {
                        ListBolt.Add(b);
                    }
                }

                Model.CommitChanges();

               

                //Dataget.component = C as Component;
                //kh.addControlpoint(p1, 1);
                //kh.addControlpoint(p3, 3);
                //kh.addControlpoint(p4, 4);


                wph.SetCurrentTransformationPlane(modelplane);
                Model.CommitChanges();


            }
            catch (Exception Exc)
            {
                MessageBox.Show(Exc.ToString());
            }

            return true;
        }


        private void GetValuesFromDialog()
        {

            if (IsDefaultValue(Data.thickness) || Data.thickness < 0)
            {
                Data.thickness = 8;
            }

            if (IsDefaultValue(Data.w) || Data.w < 0)
            {
                Data.w = 200;
            }
            if (IsDefaultValue(Data.h) || Data.h < 0)
            {
                Data.h = 200;
            }



            if (IsDefaultValue(Data.Bolt_tole) || Data.Bolt_tole < 0)
            {
                Data.Bolt_tole = 2;
            }
            if (IsDefaultValue(Data.Bolt_extra) || Data.Bolt_extra <= 0)
            {
                Data.Bolt_extra = 0;
            }

            if (IsDefaultValue(Data.Bolt_size) || Data.Bolt_size <= 0)
            {

                Data.Bolt_size = 12;
            }

            ///bolt1
            ///


            if (IsDefaultValue(Data.bx))
            {
                Data.bx = "0";

            }

            if (IsDefaultValue(Data.b1x))
            {
                Data.b1x = "0";

            }

            if (IsDefaultValue(Data.by))
            {
                Data.by = "0";

            }
            if (IsDefaultValue(Data.gap))
            {
                Data.gap = 0;
            }
            if (IsDefaultValue(Data.dy))
            {
                Data.dy = 0;
            }
            if (IsDefaultValue(Data.Bolt_tole) || Data.Bolt_tole < 0)
            {
                Data.Bolt_tole = 2;
            }
            if (IsDefaultValue(Data.Bolt_extra) || Data.Bolt_extra <= 0)
            {
                Data.Bolt_extra = 0;
            }

            if (IsDefaultValue(Data.Bolt_size) || Data.Bolt_size <= 0)
            {

                Data.Bolt_size = 12;
            }

            if (IsDefaultValue(Data.by))
            {

                Data.by = "0";
            }

            if (IsDefaultValue(Data.bx))
            {

                Data.bx = "0";
            }

        }
        #endregion

        private void Danhso(Part part)
        {

            if (IsDefaultValue(Data.paedit))
                Data.paedit = "P";
            if (IsDefaultValue(Data.panumedit))
                Data.panumedit = 1;
            if (IsDefaultValue(Data.asenumedit))
                Data.asenumedit = 1;
            if (IsDefaultValue(Data.aseedit))
                Data.aseedit = "A";
            if (IsDefaultValue(Data.coloredit))
                Data.coloredit = "6";
            if (IsDefaultValue(Data.naedit))
                Data.naedit = "BentPlate";
            if (IsDefaultValue(Data.fiedit))
                Data.fiedit = "";
            if (IsDefaultValue(Data.maedit))
                Data.maedit = "SS400";

            part.Select();
            part.PartNumber = new NumberingSeries(Data.paedit, Data.panumedit);
            part.AssemblyNumber = new NumberingSeries(Data.aseedit, Data.asenumedit);
            part.Class = Data.coloredit;
            part.Material.MaterialString = Data.maedit;
            part.Name = Data.naedit;
            part.Finish = Data.fiedit;
            part.Modify();

        }



        private Point GetCenterPointAngleSection(Beam beam)
        {
            CoordinateSystem coorbeam =
                kh.getCsRotate(beam.GetCoordinateSystem(), 135, 0, 0);

            Point p1 = beam.GetCenterLine(true)[0] as Point;
            Point p2 = beam.GetCenterLine(true)[1] as Point;
            Point CenterPoint = kh.getCenterPoint(p1, p2);

            double widthBeam = 0;
            beam.GetReportProperty("WIDTH", ref widthBeam);

            Vector vz = coorbeam.AxisX.Cross(coorbeam.AxisY);
            Point PointLineSeg2 = kh.PointPlus(CenterPoint, coorbeam.AxisY, widthBeam * 2);

            ArrayList arr = beam.GetSolid().Intersect(new LineSegment(CenterPoint, PointLineSeg2));



            Point point = arr[1] as Point;

            return point;
        }



        private Tuple<ArrayList, ArrayList> Get3PointForPolyBeam(Point p1, Point p2, CoordinateSystem coor1, CoordinateSystem coor2, double gapW, double w, int SwapVector)
        {

            Point pp1 = kh.PointPlus(p1, coor1.AxisY, gapW);
            Point pp2 = kh.PointPlus(p2, coor2.AxisX.Cross(coor2.AxisY), gapW);

            if (SwapVector == 1)
            {
                pp1 = kh.PointPlus(p1, coor1.AxisX.Cross(coor1.AxisY), gapW);
                pp2 = kh.PointPlus(p2, coor2.AxisY, gapW);
            }

            Vector v1 = kh.VectorFromPoint(p1, pp1);
            Vector v2 = kh.VectorFromPoint(p2, pp2);

            Vector vplane1 = kh.VectorFromPoint(pp1, pp2);
            Vector vplane2 = kh.VectorFromPoint(pp2, pp1);

            Point pointCenterCoo1 = kh.getCenterPoint(pp1, pp2);
            Point pointCenterCoo2 = kh.getCenterPoint(kh.PointPlus(pp1, coor1.AxisX, 100), kh.PointPlus(pp2, coor2.AxisX, 100));


            Vector vxx = kh.VectorFromPoint(pointCenterCoo1, pointCenterCoo2);
            Vector vyy = kh.VectorFromPoint(pointCenterCoo1, pp2);

            CoordinateSystem co = new CoordinateSystem(pointCenterCoo1, vxx, vyy);

            GeometricPlane plane = new GeometricPlane(co);


            Point po1 = kh.PointPlus(pp1, v1, 100);
            Point po2 = kh.PointPlus(pp2, v2, 100);


            Point pointLength1 = Projection.PointToPlane(po1, plane);
            Point pointLength2 = Projection.PointToPlane(po2, plane);

            Vector vLength1 = kh.VectorFromPoint(pp1, pointLength1);
            Vector vLength2 = kh.VectorFromPoint(pp2, pointLength2);

            Point ppp1 = kh.PointPlus(pp1, vLength1, w);
            Point ppp2 = kh.PointPlus(pp2, vLength2, w);


            //kh.addControlpoint(p1, 1);
            //kh.addControlpoint(pp1, 2);
            //kh.addControlpoint(ppp1, 3);
            //kh.addControlpoint(p2, 4);
            //kh.addControlpoint(pp2, 5);
            //kh.addControlpoint(ppp2, 6);



            ArrayList Result1 = new ArrayList();
            ArrayList Result2 = new ArrayList();
            Result1.Add(p1);
            Result1.Add(pp1);
            Result1.Add(ppp1);
            Result2.Add(p2);
            Result2.Add(pp2);
            Result2.Add(ppp2);

            return new Tuple<ArrayList, ArrayList>(Result1, Result2);
        }

        public class EventTekla
        {
            private Tekla.Structures.Model.Events _events = new Tekla.Structures.Model.Events();
            private object _selectionEventHandlerLock = new object();
            private object _changedObjectHandlerLock = new object();

            public void RegisterEventHandler()
            {
                _events.SelectionChange += Events_SelectionChangeEvent;
                _events.ModelObjectChanged += Events_ModelObjectChangedEvent;
                _events.Register();
            }

            public void UnRegisterEventHandler()
            {
                _events.UnRegister();
            }

            void Events_SelectionChangeEvent()
            {
                /* Make sure that the inner code block is running synchronously */
                lock (_selectionEventHandlerLock)
                {
                    System.Console.WriteLine("Selection changed event received.");
                }
            }

            void Events_ModelObjectChangedEvent(List<ChangeData> changes)
            {
                /* Make sure that the inner code block is running synchronously */
                lock (_changedObjectHandlerLock)
                {
                    foreach (ChangeData data in changes)
                        System.Console.WriteLine("Changed event received " + ":" + data.Object.ToString() + ":" + " Type" + ":" + data.Type.ToString() + " guid: " + data.Object.Identifier.GUID.ToString());
                    System.Console.WriteLine("Changed event received for " + changes.Count.ToString() + " objects");
                }
            }
        }
    }

}