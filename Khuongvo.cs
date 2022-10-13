using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures;
using Tekla.Structures.Datatype;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Solid;
using t3d = Tekla.Structures.Geometry3d;
using tsa = Tekla.Structures.Datatype;



namespace K_PlateOnAngleBeam
{
    public class KhuongVo
    {
        public double TotalListDouble(List<double> List)
        {
            double total = List[0];
            for (int i = 1; i < List.Count; i++)
            {
                total = List[i] + total;
            }
            return total;
        }
        public void getThickness(Beam part1, double thick)
        {
            part1.GetReportProperty("FLANGE_THICKNESS_B", ref thick);
            if (thick == 0)
            {
                part1.GetReportProperty("WEB_THICKNESS", ref thick);
                if (thick == 0)
                {
                    part1.GetReportProperty("WEB_THICKNESS_2", ref thick);
                    if (thick == 0)
                    {
                        part1.GetReportProperty("FLANGE_THICKNESS", ref thick);
                        if (thick == 0)
                        {
                            part1.GetReportProperty("FLANGE_THICKNESS_1", ref thick);

                            if (thick == 0)
                            {
                                part1.GetReportProperty("FLANGE_THICKNESS_2", ref thick);
                                if (thick == 0)
                                {
                                    part1.GetReportProperty("FLANGE_THICKNESS_U", ref thick);

                                    if (thick == 0)
                                    {
                                        string doday = "";
                                        part1.GetReportProperty("PROFILE", ref doday);
                                        string[] str2 = doday.Split('x');
                                        thick = Convert.ToDouble(str2[1]);

                                    }
                                }

                            }
                        }
                    }

                }
            }
        }


        public void getThicknessBeam(Beam part1, double thick)
        {


            string doday = "";
            part1.GetReportProperty("PROFILE", ref doday);
            string[] str2 = doday.Split('x');
            thick = Convert.ToDouble(str2[1]);



        }


        public List<double> getDistanceListPlus(string _data)
        {
            List<double> listDouble = new List<double>();

            listDouble.Clear();
            string[] str1 = _data.Trim().Split(' ');

            foreach (string item in str1)
            {

                if (item.Contains("*"))

                {
                    string[] str2 = item.Split('*');
                    int a = Convert.ToInt32(str2[0]);
                    for (int i = 0; i < a; i++)
                    {
                        if (i == 0)
                        {
                            if (listDouble.Count > 0)
                            {
                                double c = listDouble[listDouble.Count - 1];
                                listDouble.Add(Convert.ToDouble(str2[1]) + c);
                            }
                            else
                            {
                                listDouble.Add(Convert.ToDouble(str2[1]));
                            }

                        }
                        else
                        {
                            if (listDouble.Count > 0)
                            {
                                double c = listDouble[listDouble.Count - 1];
                                listDouble.Add(Convert.ToDouble(str2[1]) + c);
                            }
                        }

                    }

                }
                else
                {
                    if (listDouble.Count > 0)
                    {
                        double c = listDouble[listDouble.Count - 1];
                        listDouble.Add(Convert.ToDouble(item) + c);
                    }
                    else
                    {
                        listDouble.Add(Convert.ToDouble(item));
                    }
                }


            }

            return listDouble;

        }

        public List<double> getDistanceListThickPlus(string _data)
        {
            List<double> listDouble = new List<double>();

            listDouble.Clear();
            string[] str1 = _data.Trim().Split(' ');

            foreach (string item in str1)
            {

                if (item.Contains("x"))

                {
                    string[] str2 = item.Split('x');
                    int a = Convert.ToInt32(str2[0]);
                    for (int i = 0; i < a; i++)
                    {
                        if (i == 0)
                        {
                            if (listDouble.Count > 0)
                            {
                                double c = listDouble[listDouble.Count - 1];
                                listDouble.Add(Convert.ToDouble(str2[1]) + c);
                            }
                            else
                            {
                                listDouble.Add(Convert.ToDouble(str2[1]));
                            }

                        }
                        else
                        {
                            if (listDouble.Count > 0)
                            {
                                double c = listDouble[listDouble.Count - 1];
                                listDouble.Add(Convert.ToDouble(str2[1]) + c);
                            }
                        }

                    }

                }
                else
                {
                    if (listDouble.Count > 0)
                    {
                        double c = listDouble[listDouble.Count - 1];
                        listDouble.Add(Convert.ToDouble(item) + c);
                    }
                    else
                    {
                        listDouble.Add(Convert.ToDouble(item));
                    }
                }


            }

            return listDouble;

        }

        public List<double> GetListDoubleFormDistanceTextBox(string data)
        {
            tsa.DistanceList li = DistanceList.Parse(data, CultureInfo.CurrentCulture,
                tsa.Distance.UnitType.Millimeter);


            List<double> List = new List<double>();
            foreach (var item in li)
            {
                List.Add(item.Millimeters);
            }
            return List;

        }
        public List<double> getDistanceList(string _data)
        {
            List<double> listDouble = new List<double>();
            string[] str1 = _data.Trim().Split(' ');
            foreach (string item in str1)
            {
                if (item.Contains("*"))
                {
                    string[] str2 = item.Split('*');
                    int numb0 = Convert.ToInt32(str2[0]);
                    int numb1 = Convert.ToInt32(str2[1]);
                    if (numb0 < numb1)
                    {
                        for (int i = 0; i < numb0; i++)
                        {
                            listDouble.Add(numb1);

                        }
                    }
                    else
                    {
                        for (int i = 0; i < numb1; i++)
                        {
                            listDouble.Add(numb0);

                        }
                    }
                }
                else
                {
                    listDouble.Add(Convert.ToDouble(item));
                }

            }

            return listDouble;

        }

        public List<int> getDistanceListInt(string _data)
        {
            List<int> listDouble = new List<int>();
            string[] str1 = _data.Trim().Split(' ');
            foreach (string item in str1)
            {
                if (item.Contains("*"))
                {
                    string[] str2 = item.Split('*');
                    int numb0 = Convert.ToInt32(str2[0]);
                    int numb1 = Convert.ToInt32(str2[1]);
                    if (numb0 < numb1)
                    {
                        for (int i = 0; i < numb0; i++)
                        {
                            listDouble.Add(numb1);

                        }
                    }
                    else
                    {
                        for (int i = 0; i < numb1; i++)
                        {
                            listDouble.Add(numb0);

                        }
                    }
                }
                else
                {
                    listDouble.Add(Convert.ToInt32(item));
                }


            }

            return listDouble;

        }
        public void DrawLine(Point p1, Point p2, string i)
        {
            //  Point poi = PointOrgin;

            GraphicsDrawer draw = new GraphicsDrawer();

            draw.DrawLineSegment(p1, p2, new Color(1, 0, 0));
            draw.DrawText(p2, i, new Color(0, 0, 1));


        }
        public void DrawLine(Point p1, Point p2, double i)
        {
            //  Point poi = PointOrgin;

            GraphicsDrawer draw = new GraphicsDrawer();

            draw.DrawLineSegment(p1, p2, new Color(1, 0, 0));
            draw.DrawText(p2, "" + i, new Color(0, 0, 1));


        }

        public Point DrawTextXYZ(Point PointOrgin, double i)
        {
            //  Point poi = PointOrgin;

            GraphicsDrawer draw = new GraphicsDrawer();
            Point poidrawX = new Point(PointOrgin.X + 1000, PointOrgin.Y, PointOrgin.Z);
            Point poidrawY = new Point(PointOrgin.X, PointOrgin.Y + 1000, PointOrgin.Z);
            Point poidrawZ = new Point(PointOrgin.X, PointOrgin.Y, PointOrgin.Z + 1000);
            draw.DrawText(poidrawX, "X" + i, new Color(1, 0, 0));
            draw.DrawText(poidrawY, "Y" + i, new Color(0, 1, 0));
            draw.DrawText(poidrawZ, "Z" + i, new Color(0, 0, 1));
            draw.DrawLineSegment(PointOrgin, poidrawX, new Color(1, 0, 0));
            draw.DrawLineSegment(PointOrgin, poidrawY, new Color(0, 1, 0));
            draw.DrawLineSegment(PointOrgin, poidrawZ, new Color(0, 0, 1));
            return PointOrgin;

        }

        public Point DrawTextXYZ(CoordinateSystem coor)
        {
            //  Point poi = PointOrgin;

            GraphicsDrawer draw = new GraphicsDrawer();
            Point poidrawX = PointPlus(coor.Origin, coor.AxisX, 100);
            Point poidrawY = PointPlus(coor.Origin, coor.AxisY, 100);
            Point poidrawZ = PointPlus(coor.Origin, coor.AxisX.Cross(coor.AxisY), 100);
            draw.DrawText(poidrawX, "X", new Color(1, 0, 0));
            draw.DrawText(poidrawY, "Y", new Color(0, 1, 0));
            draw.DrawText(poidrawZ, "Z", new Color(0, 0, 1));
            draw.DrawLineSegment(coor.Origin, poidrawX, new Color(1, 0, 0));
            draw.DrawLineSegment(coor.Origin, poidrawY, new Color(0, 1, 0));
            draw.DrawLineSegment(coor.Origin, poidrawZ, new Color(0, 0, 1));
            return coor.Origin;

        }


        public Point DrawTextXYZ(CoordinateSystem coor, Point point)
        {
            //  Point poi = PointOrgin;

            GraphicsDrawer draw = new GraphicsDrawer();
            Point poidrawX = PointPlus(point, coor.AxisX, 100);
            Point poidrawY = PointPlus(point, coor.AxisY, 100);
            Point poidrawZ = PointPlus(point, coor.AxisX.Cross(coor.AxisY), 100);
            draw.DrawText(poidrawX, "X", new Color(1, 0, 0));
            draw.DrawText(poidrawY, "Y", new Color(0, 1, 0));
            draw.DrawText(poidrawZ, "Z", new Color(0, 0, 1));
            draw.DrawLineSegment(point, poidrawX, new Color(1, 0, 0));
            draw.DrawLineSegment(point, poidrawY, new Color(0, 1, 0));
            draw.DrawLineSegment(point, poidrawZ, new Color(0, 0, 1));
            return coor.Origin;

        }

        public Point DrawTextXYZ(CoordinateSystem coor, int i)
        {
            //  Point poi = PointOrgin;

            GraphicsDrawer draw = new GraphicsDrawer();
            Point poidrawX = PointPlus(coor.Origin, coor.AxisX, 100);
            Point poidrawY = PointPlus(coor.Origin, coor.AxisY, 100);
            Point poidrawZ = PointPlus(coor.Origin, coor.AxisX.Cross(coor.AxisY), 100);
            draw.DrawText(poidrawX, "X" + i, new Color(1, 0, 0));
            draw.DrawText(poidrawY, "Y" + i, new Color(0, 1, 0));
            draw.DrawText(poidrawZ, "Z" + i, new Color(0, 0, 1));
            draw.DrawLineSegment(coor.Origin, poidrawX, new Color(1, 0, 0));
            draw.DrawLineSegment(coor.Origin, poidrawY, new Color(0, 1, 0));
            draw.DrawLineSegment(coor.Origin, poidrawZ, new Color(0, 0, 1));
            return coor.Origin;

        }

        public Point DrawTextXYZ(Point PointOrgin)
        {
            //  Point poi = PointOrgin;

            GraphicsDrawer draw = new GraphicsDrawer();
            Point poidrawX = new Point(PointOrgin.X + 1000, PointOrgin.Y, PointOrgin.Z);
            Point poidrawY = new Point(PointOrgin.X, PointOrgin.Y + 1000, PointOrgin.Z);
            Point poidrawZ = new Point(PointOrgin.X, PointOrgin.Y, PointOrgin.Z + 1000);
            draw.DrawText(poidrawX, "X", new Color(1, 0, 0));
            draw.DrawText(poidrawY, "Y", new Color(0, 1, 0));
            draw.DrawText(poidrawZ, "Z", new Color(0, 0, 1));
            draw.DrawLineSegment(PointOrgin, poidrawX, new Color(1, 0, 0));
            draw.DrawLineSegment(PointOrgin, poidrawY, new Color(0, 1, 0));
            draw.DrawLineSegment(PointOrgin, poidrawZ, new Color(0, 0, 1));
            return PointOrgin;

        }

        public Point DrawText(Point PointOrgin, string Text, double i)
        {
            //  Point poi = PointOrgin;

            GraphicsDrawer draw = new GraphicsDrawer();
            draw.DrawText(PointOrgin, Text + i, new Color(1, 0, 0));


            return PointOrgin;

        }


        public ControlLine addControlLineRed(Point startPoint, Point endPoint)
        {
            ControlLine controlLine = new ControlLine(new LineSegment(startPoint, endPoint), false);
            controlLine.Extension = 0;
            controlLine.Color = ControlLine.ControlLineColorEnum.RED;
            controlLine.Insert();
            return controlLine;

        }

        public ControlLine addControlLineBlue(Point startPoint, Point endPoint)
        {
            ControlLine controlLine = new ControlLine(new LineSegment(startPoint, endPoint), false);
            controlLine.Extension = 0;
            controlLine.Color = ControlLine.ControlLineColorEnum.BLUE;
            controlLine.Insert();
            return controlLine;

        }

        public ControlLine addControlLineYellow(Point startPoint, Point endPoint)
        {
            ControlLine controlLine = new ControlLine(new LineSegment(startPoint, endPoint), false);
            controlLine.Extension = 0;
            controlLine.Color = ControlLine.ControlLineColorEnum.YELLOW;
            controlLine.Insert();
            return controlLine;

        }

        public Beam addBeam(Point startPoint, Point endPoint, string profile, string color, string material, double DepthOffset, double rotate)
        {
            Beam beam = new Beam();
            beam.StartPoint = startPoint;
            beam.EndPoint = endPoint;
            beam.Position.Depth = Position.DepthEnum.MIDDLE;
            beam.Position.Plane = Position.PlaneEnum.MIDDLE;
            beam.Position.Rotation = Position.RotationEnum.BACK;
            beam.Position.DepthOffset = DepthOffset;
            beam.Position.PlaneOffset = DepthOffset;
            beam.Position.RotationOffset = rotate;
            beam.Profile.ProfileString = profile;
            beam.Class = color;
            beam.Material.MaterialString = material;
            beam.Insert();
            return beam;
        }

        public Beam addBeam(Point startPoint, Point endPoint, string profile, string color, string name)
        {
            Beam beam = new Beam();
            beam.StartPoint = startPoint;
            beam.EndPoint = endPoint;
            beam.Position.Depth = Position.DepthEnum.MIDDLE;
            beam.Position.Plane = Position.PlaneEnum.MIDDLE;
            beam.Position.Rotation = Position.RotationEnum.BACK;
            beam.Position.DepthOffset = 0;
            beam.Position.PlaneOffset = 0;
            beam.Position.RotationOffset = 0;
            beam.Profile.ProfileString = profile;
            beam.Class = color;
            beam.Material.MaterialString = "Steel_Undefined";
            beam.Name = name;
            beam.Insert();
            return beam;
        }


        public Beam addBeam(Point startPoint, Point endPoint, string profile, string color, string name,
            Position.DepthEnum Depth, Position.PlaneEnum Plane, Position.RotationEnum Rotate,
            double offsetDepth, double PlaneOffset, double RotationOffset
            )
        {
            Beam beam = new Beam();
            beam.StartPoint = startPoint;
            beam.EndPoint = endPoint;
            beam.Position.Depth = Depth;
            beam.Position.Plane = Plane;
            beam.Position.Rotation = Rotate;
            beam.Position.DepthOffset = 0;
            beam.Position.PlaneOffset = 0;
            beam.Position.RotationOffset = 0;
            beam.Profile.ProfileString = profile;
            beam.Class = color;
            beam.Material.MaterialString = "Steel_Undefined";
            beam.Name = name;
            beam.Insert();
            return beam;
        }

        public Beam addBeam(Point startPoint, Point endPoint, string profile, string color, string name,
           Position.DepthEnum Depth, Position.PlaneEnum Plane, Position.RotationEnum Rotate)
        {
            Beam beam = new Beam();
            beam.StartPoint = startPoint;
            beam.EndPoint = endPoint;
            beam.Position.Depth = Depth;
            beam.Position.Plane = Plane;
            beam.Position.Rotation = Rotate;
            beam.Position.DepthOffset = 0;
            beam.Position.PlaneOffset = 0;
            beam.Position.RotationOffset = 0;
            beam.Profile.ProfileString = profile;
            beam.Class = color;
            beam.Material.MaterialString = "Steel_Undefined";
            beam.Name = name;
            beam.Insert();
            return beam;
        }



        public Part addBeamCut(Point startPoint, Point endPoint, string profile)
        {
            Beam beam = new Beam();
            beam.StartPoint = startPoint;
            beam.EndPoint = endPoint;
            beam.Position.Depth = Position.DepthEnum.MIDDLE;
            beam.Position.Plane = Position.PlaneEnum.MIDDLE;
            beam.Position.Rotation = Position.RotationEnum.BACK;
            beam.Position.DepthOffset = 0;
            beam.Position.PlaneOffset = 0;
            beam.Position.RotationOffset = 0;
            beam.Profile.ProfileString = profile;
            beam.Class = BooleanPart.BooleanOperativeClassName;
            beam.Material.MaterialString = "Steel_Undefined";
            beam.Insert();
            return beam as Part;

        }

        public void addBeamCut(Point startPoint, Point endPoint, string profile, ContourPlate PL)
        {
            Beam beam = new Beam();
            beam.StartPoint = startPoint;
            beam.EndPoint = endPoint;
            beam.Position.Depth = Position.DepthEnum.MIDDLE;
            beam.Position.Plane = Position.PlaneEnum.MIDDLE;
            beam.Position.Rotation = Position.RotationEnum.BACK;
            beam.Position.DepthOffset = 0;
            beam.Position.PlaneOffset = 0;
            beam.Position.RotationOffset = 0;
            beam.Profile.ProfileString = profile;
            beam.Class = BooleanPart.BooleanOperativeClassName;
            beam.Material.MaterialString = "S275";
            beam.Insert();


            BooleanPart boo = new BooleanPart();
            boo.Father = PL;
            boo.SetOperativePart(beam as Part);
            if (!boo.Insert())
                Console.WriteLine("Insert failed!");
            beam.Delete();

        }

        public BooleanPart PartCut(Part partCut, Part partFather)
        {

            BooleanPart boo = new BooleanPart();
            partFather.Select();
            boo.Father = partFather;
            boo.SetOperativePart(partCut as Part);
            if (!boo.Insert())
                Console.WriteLine("Insert failed!");

            return boo;
        }

        public Part addBeamPlate(Point startPoint, Point endPoint, int Rong, int Day)
        {
            Beam beam = new Beam();
            beam.StartPoint = startPoint;
            beam.EndPoint = endPoint;
            beam.Position.Depth = Position.DepthEnum.MIDDLE;
            beam.Position.Plane = Position.PlaneEnum.MIDDLE;
            beam.Position.Rotation = Position.RotationEnum.FRONT;
            beam.Position.DepthOffset = 0;
            beam.Position.PlaneOffset = 0;
            beam.Position.RotationOffset = 0;
            beam.Profile.ProfileString = "FL" + Rong + " * " + Day;
            beam.Class = "6";
            beam.Material.MaterialString = "S275";
            beam.Insert();

            return beam as Part;
        }

        public ContourPlate addPlate(Point Point1, Point Point2, Point Point3, Point Point4, double thickness, string color, string material, Position.DepthEnum e)
        {
            ContourPlate cPlate = new ContourPlate();
            cPlate.AddContourPoint(new ContourPoint(Point1, null));
            cPlate.AddContourPoint(new ContourPoint(Point2, null));
            cPlate.AddContourPoint(new ContourPoint(Point3, null));
            cPlate.AddContourPoint(new ContourPoint(Point4, null));
            string prolife = "PL" + Convert.ToString(thickness);
            cPlate.Profile.ProfileString = prolife;
            cPlate.Material.MaterialString = material;
            cPlate.Class = color;
            cPlate.Position.Depth = e;

            cPlate.Insert();



            return cPlate;
        }

        public ContourPlate addPlate(List<Point> Points, double thickness, string color, string material, Position.DepthEnum e)
        {
            ContourPlate cPlate = new ContourPlate();
            for (int i = 0; i < Points.Count; i++)
            {
                cPlate.AddContourPoint(new ContourPoint(Points[i], null));
            }

            string prolife = "PL" + Convert.ToString(thickness);
            cPlate.Profile.ProfileString = prolife;
            cPlate.Material.MaterialString = material;
            cPlate.Class = color;
            cPlate.Position.Depth = e;
            cPlate.Insert();



            return cPlate;
        }

        //public  double AngleBetween(Point pOrgin, Point p1, Point p2)
        //{



        //    double v1x = p1.X - pOrgin.X;
        //    double v1y = p1.Y - pOrgin.Y;
        //    double v1z = p1.Z - pOrgin.Z;

        //    Vector vector1 = new Vector(v1x, v1y, v1z);

        //    double v2x = p2.X - pOrgin.X;
        //    double v2y = p2.Y - pOrgin.Y;
        //    double v2z = p2.Z - pOrgin.Z;


        //    Vector vector2 = new Vector(v2x, v2y, v2z);

        //    double sin = vector1.X * vector2.Y - vector2.X * vector1.Y;
        //    double cos = vector1.X * vector2.X + vector1.Y * vector2.Y;

        //    return Math.Atan2(sin, cos) * (180 / Math.PI);
        //}
        public void SortPoints(ref List<Point> arrPointPlateNotSort)
        {
            List<Point> newListArr = new List<Point>();
            List<Point> newList = new List<Point>();
            List<Point> newList1 = new List<Point>();
            List<Point> newListOder = new List<Point>();
            List<Point> newListOder1 = new List<Point>();
            for (int i = 0; i < arrPointPlateNotSort.Count; i++)
            {
                Point p = arrPointPlateNotSort[i] as Point;
                if (p.Y < 0)
                {
                    newList.Add(p);
                }
                if (p.Y > 0)
                {
                    newList1.Add(p);
                }

            }

            newListOder = newList.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
            newListOder1 = newList1.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
            newListOder1.Reverse();
            newListArr.AddRange(newListOder);
            newListArr.AddRange(newListOder1);
            arrPointPlateNotSort.Clear();

            arrPointPlateNotSort.AddRange(newListArr);

        }

        public double getAngel(Point pOrgin, Point p1, Point p2)
        {
            double Goc = 0.0;
            double v1x = p1.X - pOrgin.X;
            double v1y = p1.Y - pOrgin.Y;
            double v1z = p1.Z - pOrgin.Z;

            Vector vector1 = new Vector(v1x, v1y, v1z);

            double v2x = p2.X - pOrgin.X;
            double v2y = p2.Y - pOrgin.Y;
            double v2z = p2.Z - pOrgin.Z;

            Vector vector2 = new Vector(v2x, v2y, v2z);

            double goc = vector1.GetAngleBetween(vector2);
            //Angle an = Angle.FromRadians(goc);

            Goc = goc * 180 / Math.PI;

            return Goc;
        }


        public void getPlaneForm3Point(Point PointOrg, Point pointX, Point pointY)
        {
            Model model = new Model();
            WorkPlaneHandler wph = model.GetWorkPlaneHandler();
            TransformationPlane currentPlane = wph.GetCurrentTransformationPlane();

            ControlLine LineX = new ControlLine(new LineSegment(PointOrg, pointX), false);
            LineX.Insert();


            //  ControlLine LinePlane1 = new ControlLine(new LineSegment(PointOrg, PointPlus(PointOrg, 1000, 0, 0)), false);
            //    LinePlane1.Insert();
            //GeometricPlane plane1 = new GeometricPlane(LinePlane1.GetCoordinateSystem());
            //LinePlane1.Delete();
            PointOrg = PointsToGlobal(PointOrg);
            pointX = PointsToGlobal(pointX);
            pointY = PointsToGlobal(pointY);
            wph.SetCurrentTransformationPlane(new TransformationPlane(LineX.GetCoordinateSystem())); //chuyen mat phang


            PointOrg = PointsToLocal(PointOrg);
            pointX = PointsToLocal(pointX);
            pointY = PointsToLocal(pointY);

            addControlpoint(PointOrg);
            //  addControlpoint(pointX);
            // addControlpoint(pointY);


            Point pointgiaoXoay = PointPlus(PointOrg, 0, 1000, 0);
            Line LineGiaoXoay = new Line(PointPlus(pointgiaoXoay, 0, 0, 1000), PointPlus(pointgiaoXoay, 0, 0, -1000));
            addControlpoint(pointgiaoXoay);
            ControlLine LinePlaneNgang = new ControlLine(new LineSegment(PointOrg, pointY), false);
            LinePlaneNgang.Color = ControlLine.ControlLineColorEnum.RED;
            LinePlaneNgang.Insert();
            GeometricPlane planeNgang = new GeometricPlane(LinePlaneNgang.GetCoordinateSystem());
            LinePlaneNgang.Delete();

            Point Diemxoay = Intersection.LineToPlane(LineGiaoXoay, planeNgang);
            addControlpoint(Diemxoay);
            double goc = getAngel(PointOrg, pointgiaoXoay, Diemxoay);
            DrawText(pointY, goc + "", 1);
            //wph.SetCurrentTransformationPlane(currentPlane);
            wph.SetCurrentTransformationPlane(new TransformationPlane(getCsRotate(LineX.GetCoordinateSystem(), -goc, 0, 0)));
            DrawTextXYZ(LineX.GetCoordinateSystem().Origin);
            wph.SetCurrentTransformationPlane(currentPlane);
            LineX.Delete();

        }

        public Point PlaneForm3Point(Point PointOrg, Point pointX, Point pointY)
        {
            Model model = new Model();
            WorkPlaneHandler wph = model.GetWorkPlaneHandler();
            TransformationPlane currentPlane = wph.GetCurrentTransformationPlane();

            ControlLine LineX = new ControlLine(new LineSegment(PointOrg, pointX), false);
            LineX.Insert();
            PointOrg = PointsToGlobal(PointOrg);
            pointX = PointsToGlobal(pointX);
            pointY = PointsToGlobal(pointY);
            wph.SetCurrentTransformationPlane(new TransformationPlane(LineX.GetCoordinateSystem())); //chuyen mat phang

            PointOrg = PointsToLocal(PointOrg);
            pointX = PointsToLocal(pointX);
            pointY = PointsToLocal(pointY);
            Vector vecY = new Vector(pointY);

            wph.SetCurrentTransformationPlane(new TransformationPlane(LineX.GetCoordinateSystem().Origin, LineX.GetCoordinateSystem().AxisX, vecY));
            //       DrawTextXYZ(LineX.GetCoordinateSystem().Origin);       

            Point newPoint = new Point(LineX.GetCoordinateSystem().Origin);
            LineX.Delete();
            return newPoint;

        }

        public Point PlaneForm2Point(Point PointOrg, Point pointX)
        {
            Model model = new Model();
            WorkPlaneHandler wph = model.GetWorkPlaneHandler();
            TransformationPlane currentPlane = wph.GetCurrentTransformationPlane();

            ControlLine LineX = new ControlLine(new LineSegment(PointOrg, pointX), false);
            LineX.Insert();
            PointOrg = PointsToGlobal(PointOrg);
            pointX = PointsToGlobal(pointX);

            wph.SetCurrentTransformationPlane(new TransformationPlane(LineX.GetCoordinateSystem())); //chuyen mat phang    

            Point newPoint = new Point(LineX.GetCoordinateSystem().Origin);
            LineX.Delete();
            return newPoint;

        }




        public CoordinateSystem PlaneForm3PointgetCoor(Point PointOrg, Point pointX, Point pointY)
        {
            Model model = new Model();
            WorkPlaneHandler wph = model.GetWorkPlaneHandler();
            TransformationPlane currentPlane = wph.GetCurrentTransformationPlane();

            ControlLine LineX = new ControlLine(new LineSegment(PointOrg, pointX), false);
            LineX.Insert();
            PointOrg = PointsToGlobal(PointOrg);
            pointX = PointsToGlobal(pointX);
            pointY = PointsToGlobal(pointY);
            wph.SetCurrentTransformationPlane(new TransformationPlane(LineX.GetCoordinateSystem())); //chuyen mat phang

            PointOrg = PointsToLocal(PointOrg);
            pointX = PointsToLocal(pointX);
            pointY = PointsToLocal(pointY);
            Vector vecY = new Vector(pointY);

            wph.SetCurrentTransformationPlane(new TransformationPlane(LineX.GetCoordinateSystem().Origin, LineX.GetCoordinateSystem().AxisX, vecY));
            //       DrawTextXYZ(LineX.GetCoordinateSystem().Origin);       

            Point newPoint = new Point(LineX.GetCoordinateSystem().Origin);
            LineX.Delete();

            return LineX.GetCoordinateSystem();

        }

        public ContourPlate addPlateMIDDLE(Point Point1, Point Point2, Point Point3, Point Point4, double thickness, double X1, double Y1, double X2, double Y2)
        {
            ContourPlate cPlate = new ContourPlate();
            Chamfer ch1 = new Chamfer(X1, Y1, Chamfer.ChamferTypeEnum.CHAMFER_LINE);
            Chamfer ch2 = new Chamfer(X2, Y2, Chamfer.ChamferTypeEnum.CHAMFER_LINE);

            cPlate.AddContourPoint(new ContourPoint(Point1, null));
            cPlate.AddContourPoint(new ContourPoint(Point2, null));
            cPlate.AddContourPoint(new ContourPoint(Point3, ch1));
            cPlate.AddContourPoint(new ContourPoint(Point4, ch2));
            string prolife = "PL" + Convert.ToString(thickness);
            cPlate.Profile.ProfileString = prolife;
            cPlate.Material.MaterialString = "S275";
            cPlate.Class = "6";
            cPlate.Position.Depth = Position.DepthEnum.MIDDLE;
            cPlate.Position.DepthOffset = 0;
            cPlate.Insert();



            return cPlate;
        }

        //public ContourPlate addCirclePlateFRONT(t3d.Point p, int D, int Thickness)
        //{

        //    Model model = new Model();
        //    WorkPlaneHandler whp = model.GetWorkPlaneHandler();
        //    TransformationPlane planmodel = whp.GetCurrentTransformationPlane();
        //    double R = D / 2;
        //    double c = 3;
        //    double a = -360 / c;
        //    List<t3d.Point> pointLists = new List<t3d.Point>();
        //    t3d.Point cp2 = new t3d.Point(p.X, p.Y + R, p.Z);
        //    pointLists.Add(cp2);

        //    for (int i = 0; i < c - 1; i++)
        //    {
        //        CoordinateSystem coor = new CoordinateSystem();
        //        TransformationPlane rotate = new TransformationPlane(getCsRotate(coor, 0, a, 0));
        //        whp.SetCurrentTransformationPlane(rotate);
        //        model.CommitChanges();
        //        p = PointsToLocal(p);
        //        t3d.Point cp1 = new t3d.Point(p.X, p.Y + R, p.Z);
        //        p = PointsToGlobal(p);
        //        t3d.Point plist = new t3d.Point(PointsToGlobal(cp1));
        //        pointLists.Add(plist);

        //    }

        //    whp.SetCurrentTransformationPlane(planmodel);

        //    t3d.Point p1 = pointLists[0];
        //    t3d.Point p2 = pointLists[1];
        //    t3d.Point p3 = pointLists[2];
        //    Chamfer ch1 = new Chamfer(0, 0, Chamfer.ChamferTypeEnum.CHAMFER_ARC_POINT);
        //    Chamfer ch2 = new Chamfer(0, 0, Chamfer.ChamferTypeEnum.CHAMFER_ARC_POINT);
        //    ContourPlate plate = new ContourPlate();
        //    ContourPoint cpp1 = new ContourPoint(p1, null);
        //    ContourPoint cpp2 = new ContourPoint(p2, ch2);
        //    ContourPoint cpp3 = new ContourPoint(p3, ch1);
        //    cpp1.Chamfer.DZ1 = 0;
        //    cpp1.Chamfer.DZ2 = 0;
        //    cpp2.Chamfer.DZ1 = 0;
        //    cpp2.Chamfer.DZ2 = 0;
        //    plate.AddContourPoint(cpp1);
        //    plate.AddContourPoint(cpp2);
        //    plate.AddContourPoint(cpp3);
        //    plate.Profile.ProfileString = "PL" + Thickness;
        //    plate.Material.MaterialString = "S275";
        //    plate.Class = "1";
        //    plate.Position.Depth = Position.DepthEnum.FRONT;
        //    plate.Insert();


        //    model.CommitChanges();


        //    return plate;
        //}

        //public ContourPlate addCirclePlateBEHIND(t3d.Point p, double D, int Thickness)
        //{

        //    Model model = new Model();
        //    WorkPlaneHandler whp = model.GetWorkPlaneHandler();
        //    TransformationPlane planmodel = whp.GetCurrentTransformationPlane();
        //    double R = D / 2;
        //    double c = 3;
        //    double a = -360 / c;
        //    List<t3d.Point> pointLists = new List<t3d.Point>();
        //    t3d.Point cp2 = new t3d.Point(p.X, p.Y + R, p.Z);
        //    pointLists.Add(cp2);

        //    for (int i = 0; i < c - 1; i++)
        //    {
        //        CoordinateSystem coor = new CoordinateSystem();
        //        TransformationPlane rotate = new TransformationPlane(getCsRotate(coor, 0, a, 0));
        //        whp.SetCurrentTransformationPlane(rotate);
        //        model.CommitChanges();
        //        p = PointsToLocal(p);
        //        t3d.Point cp1 = new t3d.Point(p.X, p.Y + R, p.Z);
        //        p = PointsToGlobal(p);
        //        t3d.Point plist = new t3d.Point(PointsToGlobal(cp1));
        //        pointLists.Add(plist);

        //    }

        //    whp.SetCurrentTransformationPlane(planmodel);

        //    t3d.Point p1 = pointLists[0];
        //    t3d.Point p2 = pointLists[1];
        //    t3d.Point p3 = pointLists[2];
        //    Chamfer ch1 = new Chamfer(0, 0, Chamfer.ChamferTypeEnum.CHAMFER_ARC_POINT);
        //    Chamfer ch2 = new Chamfer(0, 0, Chamfer.ChamferTypeEnum.CHAMFER_ARC_POINT);
        //    ContourPlate plate = new ContourPlate();
        //    ContourPoint cpp1 = new ContourPoint(p1, null);
        //    ContourPoint cpp2 = new ContourPoint(p2, ch2);
        //    ContourPoint cpp3 = new ContourPoint(p3, ch1);
        //    cpp1.Chamfer.DZ1 = 0;
        //    cpp1.Chamfer.DZ2 = 0;
        //    cpp2.Chamfer.DZ1 = 0;
        //    cpp2.Chamfer.DZ2 = 0;
        //    plate.AddContourPoint(cpp1);
        //    plate.AddContourPoint(cpp2);
        //    plate.AddContourPoint(cpp3);
        //    plate.Profile.ProfileString = "PL" + Thickness.ToString();
        //    plate.Material.MaterialString = "S275";
        //    plate.Class = "6";
        //    plate.Position.Depth = Position.DepthEnum.BEHIND;
        //    plate.Insert();


        //    model.CommitChanges();


        //    return plate;
        //}

        public ContourPlate addCirclePlateFRONT(t3d.Point p, double D, int Thickness)
        {

            Model model = new Model();
            WorkPlaneHandler whp = model.GetWorkPlaneHandler();
            TransformationPlane planmodel = whp.GetCurrentTransformationPlane();
            double R = D / 2;
            double c = 3;
            double a = -360 / c;
            List<t3d.Point> pointLists = new List<t3d.Point>();


            t3d.Point p1 = PointPlus(p, 0, R, 0);
            t3d.Point p2 = PointPlus(p, 0, -R, 0);
            t3d.Point p3 = PointPlus(p, 0, 0, R);
            Chamfer ch1 = new Chamfer(0, 0, Chamfer.ChamferTypeEnum.CHAMFER_ARC_POINT);
            Chamfer ch2 = new Chamfer(0, 0, Chamfer.ChamferTypeEnum.CHAMFER_ARC_POINT);
            ContourPlate plate = new ContourPlate();
            ContourPoint cpp1 = new ContourPoint(p1, null);
            ContourPoint cpp2 = new ContourPoint(p2, ch2);
            ContourPoint cpp3 = new ContourPoint(p3, ch1);
            cpp1.Chamfer.DZ1 = 0;
            cpp1.Chamfer.DZ2 = 0;
            cpp2.Chamfer.DZ1 = 0;
            cpp2.Chamfer.DZ2 = 0;
            plate.AddContourPoint(cpp1);
            plate.AddContourPoint(cpp2);
            plate.AddContourPoint(cpp3);
            plate.Profile.ProfileString = "PL" + Thickness;
            plate.Material.MaterialString = "S275";
            plate.Class = "2";
            plate.Position.Depth = Position.DepthEnum.FRONT;
            plate.Insert();


            model.CommitChanges();


            return plate;
        }

        public ContourPlate addCirclePlateBEHIND(t3d.Point p, double D, int Thickness)
        {

            Model model = new Model();
            WorkPlaneHandler whp = model.GetWorkPlaneHandler();
            TransformationPlane planmodel = whp.GetCurrentTransformationPlane();
            double R = D / 2;
            double c = 3;
            double a = -360 / c;
            List<t3d.Point> pointLists = new List<t3d.Point>();


            t3d.Point p1 = PointPlus(p, 0, R, 0);
            t3d.Point p2 = PointPlus(p, 0, -R, 0);
            t3d.Point p3 = PointPlus(p, 0, 0, R);
            Chamfer ch1 = new Chamfer(0, 0, Chamfer.ChamferTypeEnum.CHAMFER_ARC_POINT);
            Chamfer ch2 = new Chamfer(0, 0, Chamfer.ChamferTypeEnum.CHAMFER_ARC_POINT);
            ContourPlate plate = new ContourPlate();
            ContourPoint cpp1 = new ContourPoint(p1, null);
            ContourPoint cpp2 = new ContourPoint(p2, ch2);
            ContourPoint cpp3 = new ContourPoint(p3, ch1);
            cpp1.Chamfer.DZ1 = 0;
            cpp1.Chamfer.DZ2 = 0;
            cpp2.Chamfer.DZ1 = 0;
            cpp2.Chamfer.DZ2 = 0;
            plate.AddContourPoint(cpp1);
            plate.AddContourPoint(cpp2);
            plate.AddContourPoint(cpp3);
            plate.Profile.ProfileString = "PL" + Thickness;
            plate.Material.MaterialString = "S275";
            plate.Class = "2";
            plate.Position.Depth = Position.DepthEnum.BEHIND;
            plate.Insert();


            model.CommitChanges();


            return plate;
        }
        public ContourPlate addCirclePlateFRONTZ(t3d.Point p, double D, int Thickness)
        {

            Model model = new Model();
            WorkPlaneHandler whp = model.GetWorkPlaneHandler();
            TransformationPlane planmodel = whp.GetCurrentTransformationPlane();
            double R = D / 2;
            double c = 3;
            double a = -360 / c;
            List<t3d.Point> pointLists = new List<t3d.Point>();


            t3d.Point p1 = PointPlus(p, R, 0, 0);
            t3d.Point p2 = PointPlus(p, 0, -R, 0);
            t3d.Point p3 = PointPlus(p, 0, R, 0);
            Chamfer ch1 = new Chamfer(0, 0, Chamfer.ChamferTypeEnum.CHAMFER_ARC_POINT);
            Chamfer ch2 = new Chamfer(0, 0, Chamfer.ChamferTypeEnum.CHAMFER_ARC_POINT);
            ContourPlate plate = new ContourPlate();
            ContourPoint cpp1 = new ContourPoint(p1, null);
            ContourPoint cpp2 = new ContourPoint(p2, ch2);
            ContourPoint cpp3 = new ContourPoint(p3, ch1);
            cpp1.Chamfer.DZ1 = 0;
            cpp1.Chamfer.DZ2 = 0;
            cpp2.Chamfer.DZ1 = 0;
            cpp2.Chamfer.DZ2 = 0;
            plate.AddContourPoint(cpp1);
            plate.AddContourPoint(cpp2);
            plate.AddContourPoint(cpp3);
            plate.Profile.ProfileString = "PL" + Thickness;
            plate.Material.MaterialString = "S275";
            plate.Class = "2";
            plate.Position.Depth = Position.DepthEnum.FRONT;
            plate.Insert();


            model.CommitChanges();


            return plate;
        }

        public ContourPlate addCirclePlateZ(t3d.Point p, double D, int Thickness, Position.DepthEnum e, Chamfer.ChamferTypeEnum cham)
        {

            Model model = new Model();
            WorkPlaneHandler whp = model.GetWorkPlaneHandler();
            TransformationPlane planmodel = whp.GetCurrentTransformationPlane();
            double R = D / 2;
            double c = 3;
            double a = -360 / c;
            List<t3d.Point> pointLists = new List<t3d.Point>();
            t3d.Point p1 = PointPlus(p, R, 0, 0);
            t3d.Point p2 = PointPlus(p, 0, -R, 0);
            t3d.Point p3 = PointPlus(p, 0, R, 0);
            t3d.Point p4 = PointPlus(p1, -R * 2, 0, 0);

            //GraphicsDrawer dra = new GraphicsDrawer();
            //dra.DrawText(p1, "1", new Color(0, 0, 0));
            //dra.DrawText(p2, "2", new Color(0, 0, 0));
            //dra.DrawText(p3, "3", new Color(0, 0, 0));
            //dra.DrawText(p4, "4", new Color(0, 0, 0));
            Chamfer ch1 = new Chamfer(0, 0, cham);
            Chamfer ch2 = new Chamfer(0, 0, cham);
            ContourPlate plate = new ContourPlate();
            ContourPoint cpp1 = new ContourPoint(p1, ch1);
            ContourPoint cpp2 = new ContourPoint(p2, null);
            ContourPoint cpp3 = new ContourPoint(p3, null);
            ContourPoint cpp4 = new ContourPoint(p4, ch2);
            cpp1.Chamfer.DZ1 = 0;
            cpp1.Chamfer.DZ2 = 0;
            cpp2.Chamfer.DZ1 = 0;
            cpp2.Chamfer.DZ2 = 0;
            plate.AddContourPoint(cpp1);
            plate.AddContourPoint(cpp2);
            plate.AddContourPoint(cpp4);
            plate.AddContourPoint(cpp3);
            plate.Profile.ProfileString = "PL" + Thickness;
            plate.Material.MaterialString = "S275";
            plate.Class = "2";
            plate.Position.Depth = e;
            plate.Insert();


            model.CommitChanges();


            return plate;
        }

        public ContourPlate addPlateZCut(t3d.Point p, double D, int Thickness, Position.DepthEnum e)
        {

            Model model = new Model();
            WorkPlaneHandler whp = model.GetWorkPlaneHandler();
            TransformationPlane planmodel = whp.GetCurrentTransformationPlane();
            double R = D / 2;
            double c = 3;
            double a = -360 / c;
            List<t3d.Point> pointLists = new List<t3d.Point>();
            t3d.Point p1 = PointPlus(p, R, 0, 0);
            t3d.Point p2 = PointPlus(p, 0, -R, 0);
            t3d.Point p3 = PointPlus(p, 0, R, 0);
            t3d.Point p4 = PointPlus(p1, -R * 2, 0, 0);

            //GraphicsDrawer dra = new GraphicsDrawer();
            //dra.DrawText(p1, "1", new Color(0, 0, 0));
            //dra.DrawText(p2, "2", new Color(0, 0, 0));
            //dra.DrawText(p3, "3", new Color(0, 0, 0));
            //dra.DrawText(p4, "4", new Color(0, 0, 0));

            ContourPlate plate = new ContourPlate();
            ContourPoint cpp1 = new ContourPoint(p1, null);
            ContourPoint cpp2 = new ContourPoint(p2, null);
            ContourPoint cpp3 = new ContourPoint(p3, null);
            ContourPoint cpp4 = new ContourPoint(p4, null);
            cpp1.Chamfer.DZ1 = 0;
            cpp1.Chamfer.DZ2 = 0;
            cpp2.Chamfer.DZ1 = 0;
            cpp2.Chamfer.DZ2 = 0;
            plate.AddContourPoint(cpp1);
            plate.AddContourPoint(cpp2);
            plate.AddContourPoint(cpp4);
            plate.AddContourPoint(cpp3);
            plate.Profile.ProfileString = "PL" + Thickness;
            plate.Material.MaterialString = "S275";
            plate.Class = BooleanPart.BooleanOperativeClassName;
            plate.Position.Depth = e;
            plate.Insert();


            model.CommitChanges();


            return plate;
        }


        public void PolygonCut(ContourPlate PL, ArrayList arr, int Thickness)
        {
            ContourPlate plate = new ContourPlate();

            foreach (var item in arr)
            {
                ContourPoint pp1 = new ContourPoint(item as t3d.Point, null);
                plate.AddContourPoint(pp1);
            }
            plate.Material.MaterialString = "S275";
            plate.Profile.ProfileString = "PL" + Thickness.ToString();
            plate.Class = BooleanPart.BooleanOperativeClassName;
            plate.Position.Depth = Position.DepthEnum.MIDDLE;
            plate.Insert();

            BooleanPart boo = new BooleanPart();
            boo.Father = PL;
            boo.SetOperativePart(plate as Part);
            if (!boo.Insert())
                Console.WriteLine("Insert failed!");
            plate.Delete();

        }



        public Beam PartCut(Part part1, Point point1, double Thickness, double number)
        {
            Beam b = new Beam();
            b.StartPoint = new Point(point1.X, point1.Y, point1.Z - Thickness);
            b.EndPoint = new Point(point1.X, point1.Y, point1.Z + Thickness);
            b.Material.MaterialString = "S275";
            b.Profile.ProfileString = "FL" + number.ToString() + "*" + number.ToString();
            b.Class = BooleanPart.BooleanOperativeClassName;
            b.Position.Depth = Position.DepthEnum.MIDDLE;
            b.Insert();

            BooleanPart boo = new BooleanPart();
            part1.Select();
            boo.Father = part1;
            boo.SetOperativePart(b as Part);
            if (!boo.Insert())
                Console.WriteLine("Insert failed!");
            b.Delete();
            return b;
        }

        public Beam PartCutHoles(ContourPlate part1, Point point1, int Thickness, int numberholes)
        {
            Beam b = new Beam();
            b.StartPoint = new Point(point1.X, point1.Y, point1.Z - Thickness);
            b.EndPoint = new Point(point1.X, point1.Y, point1.Z + Thickness);
            b.Material.MaterialString = "S275";
            b.Profile.ProfileString = "D" + numberholes.ToString();
            b.Class = BooleanPart.BooleanOperativeClassName;
            b.Position.Depth = Position.DepthEnum.MIDDLE;
            b.Insert();

            BooleanPart boo = new BooleanPart();
            part1.Select();
            boo.Father = part1;
            boo.SetOperativePart(b);
            if (!boo.Insert())
                Console.WriteLine("Insert failed!");
            b.Delete();
            return b;
        }
        public ControlPoint addControlpoint(Point p)
        {

            ControlPoint cp = new ControlPoint(p);
            cp.Insert();
            return cp;
        }
        public ControlPoint addControlpoint(Point p, double i)
        {

            ControlPoint cp = new ControlPoint(p);
            cp.Insert();

            GraphicsDrawer dra = new GraphicsDrawer();
            dra.DrawText(cp.Point, "" + i, new Color(1, 0, 0));


            return cp;
        }
        public ControlPoint addControlpoint(Point p, string str, double i)
        {

            ControlPoint cp = new ControlPoint(p);
            cp.Insert();

            GraphicsDrawer dra = new GraphicsDrawer();
            dra.DrawText(cp.Point, str + i, new Color(1, 0, 0));


            return cp;
        }

        public ControlPoint addControlpoint(Point p, string str)
        {

            ControlPoint cp = new ControlPoint(p);
            cp.Insert();

            GraphicsDrawer dra = new GraphicsDrawer();
            dra.DrawText(cp.Point, str, new Color(1, 0, 0));


            return cp;
        }
        public Assembly addWeld(Part mainPart, Part secondaryObject)
        {
            Weld Weld = new Weld();
            Weld.MainObject = mainPart;
            Weld.SecondaryObject = secondaryObject;
            Weld.TypeAbove = BaseWeld.WeldTypeEnum.WELD_TYPE_SQUARE_GROOVE_SQUARE_BUTT;
            Weld.Insert();
            Assembly assembly = mainPart.GetAssembly();
            assembly.Add(secondaryObject);
            assembly.Modify();
            return assembly;

        }



        internal void addCircleBolt(ContourPlate mainplate, ContourPlate seconplate,
            string boltstandard, double BoltSize, double Bolttole, double Boltcut, double numberbolt, double dim)
        {
            BoltCircle B = new BoltCircle();

            B.PartToBeBolted = mainplate;
            B.PartToBoltTo = seconplate;

            B.FirstPosition = new Point(9000, 6000, 0);
            B.SecondPosition = new Point(12000, 12000, 0);

            B.BoltSize = BoltSize;
            B.Tolerance = Bolttole;
            B.BoltStandard = boltstandard;
            B.BoltType = BoltGroup.BoltTypeEnum.BOLT_TYPE_WORKSHOP;
            B.CutLength = 105;

            B.Length = 100;
            B.ExtraLength = Boltcut;
            B.ThreadInMaterial = BoltGroup.BoltThreadInMaterialEnum.THREAD_IN_MATERIAL_NO;

            B.Position.Depth = Position.DepthEnum.MIDDLE;
            B.Position.Plane = Position.PlaneEnum.MIDDLE;
            B.Position.Rotation = Position.RotationEnum.FRONT;

            B.Bolt = true;
            B.Washer1 = true;
            B.Washer2 = true;
            B.Washer3 = true;
            B.Nut1 = true;
            B.Nut2 = true;

            B.Hole1 = true;
            B.Hole2 = true;
            B.Hole3 = true;
            B.Hole4 = true;
            B.Hole5 = true;

            B.NumberOfBolts = numberbolt;
            B.Diameter = dim;
            B.Insert();

        }

        public ContourPlate addCirclePlateMIDDLE(t3d.Point p, double D, int Thickness)
        {

            Model model = new Model();
            WorkPlaneHandler whp = model.GetWorkPlaneHandler();
            TransformationPlane planmodel = whp.GetCurrentTransformationPlane();
            double R = D / 2;
            double c = 3;
            double a = -360 / c;
            List<t3d.Point> pointLists = new List<t3d.Point>();
            t3d.Point cp2 = new t3d.Point(p.X, p.Y + R, p.Z);
            pointLists.Add(cp2);

            for (int i = 0; i < c - 1; i++)
            {
                CoordinateSystem coor = new CoordinateSystem();
                TransformationPlane rotate = new TransformationPlane(getCsRotate(coor, 0, 0, a));
                whp.SetCurrentTransformationPlane(rotate);
                model.CommitChanges();
                p = PointsToLocal(p);
                t3d.Point cp1 = new t3d.Point(p.X, p.Y + R, p.Z);
                p = PointsToGlobal(p);
                t3d.Point plist = new t3d.Point(PointsToGlobal(cp1));
                pointLists.Add(plist);

            }

            whp.SetCurrentTransformationPlane(planmodel);

            t3d.Point p1 = pointLists[0];
            t3d.Point p2 = pointLists[1];
            t3d.Point p3 = pointLists[2];
            Chamfer ch1 = new Chamfer(0, 0, Chamfer.ChamferTypeEnum.CHAMFER_ARC_POINT);
            Chamfer ch2 = new Chamfer(0, 0, Chamfer.ChamferTypeEnum.CHAMFER_ARC_POINT);
            ContourPlate plate = new ContourPlate();
            ContourPoint cpp1 = new ContourPoint(p1, null);
            ContourPoint cpp2 = new ContourPoint(p2, ch2);
            ContourPoint cpp3 = new ContourPoint(p3, ch1);
            cpp1.Chamfer.DZ1 = 0;
            cpp1.Chamfer.DZ2 = 0;
            cpp2.Chamfer.DZ1 = 0;
            cpp2.Chamfer.DZ2 = 0;
            plate.AddContourPoint(cpp1);
            plate.AddContourPoint(cpp2);
            plate.AddContourPoint(cpp3);
            plate.Profile.ProfileString = "PL" + Thickness.ToString();
            plate.Material.MaterialString = "S275";
            plate.Class = "1";
            plate.Position.Depth = Position.DepthEnum.MIDDLE;
            plate.Insert();


            model.CommitChanges();


            return plate;
        }


        public ContourPlate addCirclePlateMIDDLETop(t3d.Point p, double D, int Thickness)
        {

            Model model = new Model();
            WorkPlaneHandler whp = model.GetWorkPlaneHandler();
            TransformationPlane planmodel = whp.GetCurrentTransformationPlane();
            double R = D / 2;
            double c = 3;
            double a = -360 / c;
            List<t3d.Point> pointLists = new List<t3d.Point>();
            t3d.Point cp2 = new t3d.Point(p.X, p.Y + R, p.Z);
            pointLists.Add(cp2);

            for (int i = 0; i < c - 1; i++)
            {
                CoordinateSystem coor = new CoordinateSystem();
                TransformationPlane rotate = new TransformationPlane(getCsRotate(coor, a, 0, 0));
                whp.SetCurrentTransformationPlane(rotate);
                model.CommitChanges();
                p = PointsToLocal(p);
                t3d.Point cp1 = new t3d.Point(p.X, p.Y + R, p.Z);
                p = PointsToGlobal(p);
                t3d.Point plist = new t3d.Point(PointsToGlobal(cp1));
                pointLists.Add(plist);

            }

            whp.SetCurrentTransformationPlane(planmodel);

            t3d.Point p1 = pointLists[0];
            t3d.Point p2 = pointLists[1];
            t3d.Point p3 = pointLists[2];
            Chamfer ch1 = new Chamfer(0, 0, Chamfer.ChamferTypeEnum.CHAMFER_ARC_POINT);
            Chamfer ch2 = new Chamfer(0, 0, Chamfer.ChamferTypeEnum.CHAMFER_ARC_POINT);
            ContourPlate plate = new ContourPlate();
            ContourPoint cpp1 = new ContourPoint(p1, null);
            ContourPoint cpp2 = new ContourPoint(p2, ch2);
            ContourPoint cpp3 = new ContourPoint(p3, ch1);
            cpp1.Chamfer.DZ1 = 0;
            cpp1.Chamfer.DZ2 = 0;
            cpp2.Chamfer.DZ1 = 0;
            cpp2.Chamfer.DZ2 = 0;
            plate.AddContourPoint(cpp1);
            plate.AddContourPoint(cpp2);
            plate.AddContourPoint(cpp3);
            plate.Profile.ProfileString = "PL" + Thickness.ToString();
            plate.Material.MaterialString = "S275";
            plate.Class = "1";
            plate.Position.Depth = Position.DepthEnum.MIDDLE;
            plate.Insert();


            model.CommitChanges();


            return plate;
        }


        public void cutPlane(Part part, Point point)
        {
            CutPlane cut = new CutPlane();
            cut.Plane = new Plane();
            cut.Plane.Origin = point;
            cut.Plane.AxisX = new Vector(point.X, 1000, 0);
            cut.Plane.AxisY = new Vector(point.X, +1000, 0);
            cut.Father = part;
            cut.Insert();

        }

        public GeometricPlane CreatePlaneFromBeam(Beam part)
        {

            Vector vecX = part.GetCoordinateSystem().AxisX;
            Vector vecY = vecX.Cross(new Vector(0, 0, 1));
            Point point = getCenterPoint(part.StartPoint, part.EndPoint);
            point = PointPlus(point, 10, 0, 0);
            CoordinateSystem coor = getCsRotate(new CoordinateSystem(point, vecX, vecY), 0, 90, 0);
            GeometricPlane plan = new GeometricPlane(point, coor.AxisX, coor.AxisY);

            return plan;

        }


        public CoordinateSystem GetCoordinateSystemDirectionBeam2(Beam b1, Beam b2)
        {
            t3d.GeometricPlane plane = CreatePlaneFromBeam(b2);
            t3d.Point pp = t3d.Projection.PointToPlane(b1.StartPoint, plane);
            t3d.Vector vx = VectorFromPoint(b1.StartPoint, b2.EndPoint);
            t3d.Line lineX = new t3d.Line(pp, PointPlus(pp, vx, t3d.Distance.PointToPoint(b1.StartPoint, b2.EndPoint)));
            t3d.Point pp1 = t3d.Projection.PointToLine(b1.StartPoint, lineX);
            t3d.Vector vy = VectorFromPoint(b1.StartPoint, pp1);
            t3d.CoordinateSystem coor = new t3d.CoordinateSystem(b1.StartPoint, vx, vy);

            return coor;

        }


        public void fitting(Beam part, Point point, double OffsetPointCut)
        {
            double kc1 = t3d.Distance.PointToPoint(point, part.StartPoint);
            double kc2 = t3d.Distance.PointToPoint(point, part.EndPoint);
            Vector vx = new Vector();
            if (kc1 < kc2)
            {
                vx = VectorFromPoint(part.StartPoint, part.EndPoint);
                point = PointPlus(point, vx, -OffsetPointCut);
            }
            else
            {
                vx = VectorFromPoint(part.EndPoint, part.StartPoint);
                point = PointPlus(point, vx, -OffsetPointCut);
            }

            Vector vecX = part.GetCoordinateSystem().AxisX;
            Vector vecY = vecX.Cross(new Vector(0, 0, 1));

            CoordinateSystem coor = getCsRotate(new CoordinateSystem(point, vecX, vecY), 0, 90, 0);
            Fitting fit = new Fitting();
            fit.Plane = new Plane();
            fit.Plane.Origin = point;
            fit.Plane.AxisX = coor.AxisX;
            fit.Plane.AxisY = coor.AxisY;
            fit.Father = part;
            fit.Insert();

        }

        public Tuple<t3d.Point, t3d.Point, t3d.Point, t3d.Point, t3d.Point, t3d.Point, t3d.Point, t3d.Point> add_TamGiacDeu(t3d.Point center_Point, double bottomWidth, double topWidth, double bottomHeight, double topHeight)
        {



            double D = bottomWidth / Math.Sqrt(3);


            ControlPoint bottom_CenterPoint = new ControlPoint(center_Point);
            bottom_CenterPoint.Insert();
            t3d.Point bottomPoint_1 = new t3d.Point(center_Point.X, center_Point.Y + D, bottomHeight);
            t3d.Point p2 = new t3d.Point(center_Point.X + D, center_Point.Y, bottomHeight);
            t3d.Point p3 = new t3d.Point(center_Point.X - D, center_Point.Y, bottomHeight);
            t3d.Point p4 = new t3d.Point(center_Point.X, center_Point.Y - D / 2, bottomHeight);
            t3d.LineSegment l1_bottom = new t3d.LineSegment(p4, new t3d.Point(p4.X + bottomWidth / 2, p4.Y, bottomHeight));
            t3d.LineSegment l2_bottom = new t3d.LineSegment(p4, new t3d.Point(p4.X - bottomWidth / 2, p4.Y, bottomHeight));
            t3d.LineSegment l3_bottom = new t3d.LineSegment(l1_bottom.Point2, l2_bottom.Point2);
            ControlLine line1_bottom = new ControlLine(l3_bottom, false);
            ControlLine line2_bottom = new ControlLine(new t3d.LineSegment(bottomPoint_1, line1_bottom.Line.Point2), false);
            ControlLine line3_bottom = new ControlLine(new t3d.LineSegment(bottomPoint_1, line1_bottom.Line.Point1), false);
            line1_bottom.Extension = 0;
            line2_bottom.Extension = 0;
            line3_bottom.Extension = 0;
            line1_bottom.Insert();
            line2_bottom.Insert();
            line3_bottom.Insert();
            t3d.Point bottomPoint_2 = new t3d.Point(line1_bottom.Line.Point2);
            t3d.Point bottomPoint_3 = new t3d.Point(line1_bottom.Line.Point1);


            D = topWidth / Math.Sqrt(3);


            ControlPoint top_CenterPoint = new ControlPoint(center_Point);
            top_CenterPoint.Insert();
            t3d.Point topPoint_1 = new t3d.Point(center_Point.X, center_Point.Y + D, bottomHeight);
            t3d.Point p2_top = new t3d.Point(center_Point.X + D, center_Point.Y, bottomHeight);
            t3d.Point p3_top = new t3d.Point(center_Point.X - D, center_Point.Y, bottomHeight);
            t3d.Point p4_top = new t3d.Point(center_Point.X, center_Point.Y - D / 2, bottomHeight);
            t3d.LineSegment l1_top = new t3d.LineSegment(p4, new t3d.Point(p4.X + bottomWidth / 2, p4.Y, bottomHeight));
            t3d.LineSegment l2_top = new t3d.LineSegment(p4, new t3d.Point(p4.X - bottomWidth / 2, p4.Y, bottomHeight));
            t3d.LineSegment l3_top = new t3d.LineSegment(l1_top.Point2, l2_top.Point2);
            ControlLine line1_top = new ControlLine(l3_top, false);
            ControlLine line2_top = new ControlLine(new t3d.LineSegment(bottomPoint_1, line1_top.Line.Point2), false);
            ControlLine line3_top = new ControlLine(new t3d.LineSegment(bottomPoint_1, line1_top.Line.Point1), false);
            line1_top.Extension = 0;
            line2_top.Extension = 0;
            line3_top.Extension = 0;
            line1_top.Insert();
            line2_top.Insert();
            line3_top.Insert();
            t3d.Point topPoint_2 = new t3d.Point(line1_top.Line.Point2);
            t3d.Point topPoint_3 = new t3d.Point(line1_top.Line.Point1);

            return new Tuple<t3d.Point, t3d.Point, t3d.Point, t3d.Point, t3d.Point, t3d.Point, t3d.Point, t3d.Point>(bottom_CenterPoint.Point, bottomPoint_1, bottomPoint_2, bottomPoint_3,
                                                                             top_CenterPoint.Point, topPoint_1, topPoint_2, topPoint_3);


        }
        public Triangle add2Triangle(t3d.Point center_Point, double bottomWidth, double topWidth, double bottomHeight, double topHeight)
        {

            double D = bottomWidth / Math.Sqrt(3);
            ControlPoint bottom_CenterPoint = new ControlPoint(center_Point);
            bottom_CenterPoint.Insert();
            t3d.Point bottomPoint_1 = new t3d.Point(center_Point.X, center_Point.Y + D, bottomHeight);

            //  t3d.Point p2 = new t3d.Point(center_Point.X + D, center_Point.Y, bottomHeight);
            // t3d.Point p3 = new t3d.Point(center_Point.X - D, center_Point.Y, bottomHeight);
            t3d.Point p4_bottom = new t3d.Point(center_Point.X, center_Point.Y - D / 2, bottomHeight);
            t3d.Point bottomPoint_2 = new t3d.Point(p4_bottom.X + bottomWidth / 2, p4_bottom.Y, bottomHeight);
            t3d.Point bottomPoint_3 = new t3d.Point(p4_bottom.X - bottomWidth / 2, p4_bottom.Y, bottomHeight);
            ControlLine line1_bottom = new ControlLine(new t3d.LineSegment(bottomPoint_1, bottomPoint_2), false);
            ControlLine line2_bottom = new ControlLine(new t3d.LineSegment(bottomPoint_2, bottomPoint_3), false);
            ControlLine line3_bottom = new ControlLine(new t3d.LineSegment(bottomPoint_3, bottomPoint_1), false);
            line1_bottom.Extension = 0;
            line2_bottom.Extension = 0;
            line3_bottom.Extension = 0;
            line1_bottom.Insert();
            line2_bottom.Insert();
            line3_bottom.Insert();

            D = topWidth / Math.Sqrt(3);

            t3d.Point topPoint = new Point(center_Point.X, center_Point.Y, center_Point.Z + topHeight);

            ControlPoint top_CenterPoint = new ControlPoint(topPoint);
            top_CenterPoint.Insert();
            t3d.Point topPoint_1 = new t3d.Point(topPoint.X, topPoint.Y + D, topHeight);
            t3d.Point p4_top = new t3d.Point(topPoint.X, topPoint.Y - D / 2, topHeight);
            t3d.Point topPoint_2 = new t3d.Point(p4_top.X + topWidth / 2, p4_top.Y, topHeight);
            t3d.Point topPoint_3 = new t3d.Point(p4_top.X - topWidth / 2, p4_top.Y, topHeight);
            ControlLine line1_top = new ControlLine(new t3d.LineSegment(topPoint_1, topPoint_2), false);
            ControlLine line2_top = new ControlLine(new t3d.LineSegment(topPoint_2, topPoint_3), false);
            ControlLine line3_top = new ControlLine(new t3d.LineSegment(topPoint_3, topPoint_1), false);
            line1_top.Extension = 0;
            line2_top.Extension = 0;
            line3_top.Extension = 0;
            line1_top.Insert();
            line2_top.Insert();
            line3_top.Insert();


            return new Triangle
            {
                bottom_CenterPoint = new t3d.Point(bottom_CenterPoint.Point),
                bottomPoint_Top = new t3d.Point(bottomPoint_1),
                bottomPoint_Right = new t3d.Point(bottomPoint_2),
                bottomPoint_Left = new t3d.Point(bottomPoint_3),
                top_CenterPoint = new t3d.Point(top_CenterPoint.Point),
                topPoint_Top = new t3d.Point(topPoint_1),
                topPoint_Right = new t3d.Point(topPoint_2),
                topPoint_Left = new t3d.Point(topPoint_3)


            };
        }

        public struct Triangle
        {
            public t3d.Point bottom_CenterPoint;
            public t3d.Point bottomPoint_Top;
            public t3d.Point bottomPoint_Right;
            public t3d.Point bottomPoint_Left;
            public t3d.Point top_CenterPoint;
            public t3d.Point topPoint_Top;
            public t3d.Point topPoint_Right;
            public t3d.Point topPoint_Left;



        }


        public recTang add2recTang(t3d.Point center_Point, double bottomWidth, double bottomWidth1, double topWidth, double topWidth1, double bottomHeight, double topHeight)
        {

            double D = bottomWidth / 2;
            double D1 = bottomWidth1 / 2;
            double D2 = topWidth / 2;
            double D3 = topWidth1 / 2;
            ControlPoint bottom_CenterPoint = new ControlPoint(center_Point);
            bottom_CenterPoint.Insert();
            t3d.Point bottom_le1 = new t3d.Point(center_Point.X - D, center_Point.Y + D1, bottomHeight);
            t3d.Point bottom_le2 = new t3d.Point(center_Point.X + D, center_Point.Y + D1, bottomHeight);
            t3d.Point bottom_le3 = new t3d.Point(center_Point.X + D, center_Point.Y - D1, bottomHeight);
            t3d.Point bottom_le4 = new t3d.Point(center_Point.X - D, center_Point.Y - D1, bottomHeight);


            ControlLine line1_bottom = new ControlLine(new t3d.LineSegment(bottom_le1, bottom_le2), false);
            ControlLine line2_bottom = new ControlLine(new t3d.LineSegment(bottom_le2, bottom_le3), false);
            ControlLine line3_bottom = new ControlLine(new t3d.LineSegment(bottom_le3, bottom_le4), false);
            ControlLine line4_bottom = new ControlLine(new t3d.LineSegment(bottom_le4, bottom_le1), false);
            line1_bottom.Extension = 0;
            line2_bottom.Extension = 0;
            line3_bottom.Extension = 0;
            line4_bottom.Extension = 0;
            line1_bottom.Insert();
            line2_bottom.Insert();
            line3_bottom.Insert();
            line4_bottom.Insert();


            ControlPoint top_CenterPoint = new ControlPoint(new t3d.Point(center_Point.X, center_Point.Y, center_Point.Z + topHeight));

            top_CenterPoint.Insert();


            t3d.Point top_le1 = new t3d.Point(center_Point.X - D2, center_Point.Y + D3, topHeight);
            t3d.Point top_le2 = new t3d.Point(center_Point.X + D2, center_Point.Y + D3, topHeight);
            t3d.Point top_le3 = new t3d.Point(center_Point.X + D2, center_Point.Y - D3, topHeight);
            t3d.Point top_le4 = new t3d.Point(center_Point.X - D2, center_Point.Y - D3, topHeight);


            ControlLine line1_top = new ControlLine(new t3d.LineSegment(top_le1, top_le2), false);
            ControlLine line2_top = new ControlLine(new t3d.LineSegment(top_le2, top_le3), false);
            ControlLine line3_top = new ControlLine(new t3d.LineSegment(top_le3, top_le4), false);
            ControlLine line4_top = new ControlLine(new t3d.LineSegment(top_le4, top_le1), false);
            line1_top.Extension = 0;
            line2_top.Extension = 0;
            line3_top.Extension = 0;
            line4_top.Extension = 0;
            line1_top.Insert();
            line2_top.Insert();
            line3_top.Insert();
            line4_top.Insert();



            return new recTang
            {
                bottom_CenterPoint = new t3d.Point(bottom_CenterPoint.Point),
                bottom_1 = new t3d.Point(bottom_le1),
                bottom_2 = new t3d.Point(bottom_le2),
                bottom_3 = new t3d.Point(bottom_le3),
                bottom_4 = new t3d.Point(bottom_le4),
                top_CenterPoint = new t3d.Point(top_CenterPoint.Point),
                topPoint_1 = new t3d.Point(top_le1),
                topPoint_2 = new t3d.Point(top_le2),
                topPoint_3 = new t3d.Point(top_le3),
                topPoint_4 = new t3d.Point(top_le4)

            };
        }

        public struct recTang
        {
            public t3d.Point bottom_CenterPoint;
            public t3d.Point bottom_1;
            public t3d.Point bottom_2;
            public t3d.Point bottom_3;
            public t3d.Point bottom_4;
            public t3d.Point top_CenterPoint;
            public t3d.Point topPoint_1;
            public t3d.Point topPoint_2;
            public t3d.Point topPoint_3;
            public t3d.Point topPoint_4;



        }



        public mid3Point addTriangleWithIntersection(double Height, t3d.Point bottom_CenterPoint, t3d.Point bottomPoint_1, t3d.Point bottomPoint_2, t3d.Point bottomPoint_3,
      t3d.Point topPoint_1, t3d.Point topPoint_2, t3d.Point topPoint_3)
        {

            t3d.Point centerPoint = new t3d.Point(bottom_CenterPoint.X, bottom_CenterPoint.Y, bottom_CenterPoint.Z + Height);
            ControlPoint point_centerPoint = new ControlPoint(centerPoint);
            point_centerPoint.Insert();
            t3d.Point p1 = new t3d.Point(bottomPoint_1.X, bottomPoint_1.Y, bottomPoint_1.Z + Height);
            t3d.Point p2 = new t3d.Point(bottomPoint_2.X, bottomPoint_2.Y, bottomPoint_2.Z + Height);
            t3d.Point p3 = new t3d.Point(bottomPoint_3.X, bottomPoint_3.Y, bottomPoint_3.Z + Height);
            ControlLine cpline = addControlLineRed(centerPoint, p1);
            GeometricPlane geoPlane = new GeometricPlane(cpline.GetCoordinateSystem());
            t3d.Point pp1 = Intersection.LineToPlane(new Line(bottomPoint_1, topPoint_1), geoPlane);
            t3d.Point pp2 = Intersection.LineToPlane(new Line(bottomPoint_2, topPoint_2), geoPlane);
            t3d.Point pp3 = Intersection.LineToPlane(new Line(bottomPoint_3, topPoint_3), geoPlane);
            cpline.Delete();



            return new mid3Point
            {
                midPoint_Top = new Point(pp1),
                midPoint_Right = new Point(pp2),
                midPoint_Left = new Point(pp3),
                midPoint_Center = new Point(centerPoint)

            };

            //   kh.addControlLine(midPoint_Top, midPoint_Right);
            //   kh.addControlLine(midPoint_Right, midPoint3_Left);
            //   kh.addControlLine(midPoint3_Left, midPoint_Top);
        }

        public struct mid3Point
        {
            public t3d.Point midPoint_Top;
            public t3d.Point midPoint_Right;
            public t3d.Point midPoint_Left;
            public t3d.Point midPoint_Center;

        }

        public mid4Point addRectangWithIntersection(double Height, t3d.Point bottom_CenterPoint, t3d.Point bottomPoint_1, t3d.Point bottomPoint_2, t3d.Point bottomPoint_3, t3d.Point bottomPoint_4,
 t3d.Point topPoint_1, t3d.Point topPoint_2, t3d.Point topPoint_3, t3d.Point topPoint_4)
        {

            t3d.Point centerPoint = new t3d.Point(bottom_CenterPoint.X, bottom_CenterPoint.Y, bottom_CenterPoint.Z + Height);
            ControlPoint point_centerPoint = new ControlPoint(centerPoint);
            point_centerPoint.Insert();
            t3d.Point p1 = new t3d.Point(bottomPoint_1.X, bottomPoint_1.Y, bottomPoint_1.Z + Height);
            t3d.Point p2 = new t3d.Point(bottomPoint_2.X, bottomPoint_2.Y, bottomPoint_2.Z + Height);
            t3d.Point p3 = new t3d.Point(bottomPoint_3.X, bottomPoint_3.Y, bottomPoint_3.Z + Height);
            t3d.Point p4 = new t3d.Point(bottomPoint_4.X, bottomPoint_4.Y, bottomPoint_4.Z + Height);
            ControlLine cpline = addControlLineRed(centerPoint, p1);
            GeometricPlane geoPlane = new GeometricPlane(cpline.GetCoordinateSystem());
            t3d.Point pp1 = Intersection.LineToPlane(new Line(bottomPoint_1, topPoint_1), geoPlane);
            t3d.Point pp2 = Intersection.LineToPlane(new Line(bottomPoint_2, topPoint_2), geoPlane);
            t3d.Point pp3 = Intersection.LineToPlane(new Line(bottomPoint_3, topPoint_3), geoPlane);
            t3d.Point pp4 = Intersection.LineToPlane(new Line(bottomPoint_4, topPoint_4), geoPlane);
            cpline.Delete();



            return new mid4Point
            {
                midPoint_1 = new Point(pp1),
                midPoint_2 = new Point(pp2),
                midPoint_3 = new Point(pp3),
                midPoint_4 = new Point(pp4),
                midPoint_Center = new Point(centerPoint)

            };

            //   kh.addControlLine(midPoint_Top, midPoint_Right);
            //   kh.addControlLine(midPoint_Right, midPoint3_Left);
            //   kh.addControlLine(midPoint3_Left, midPoint_Top);
        }

        public struct mid4Point
        {
            public t3d.Point midPoint_1;
            public t3d.Point midPoint_2;
            public t3d.Point midPoint_3;
            public t3d.Point midPoint_4;
            public t3d.Point midPoint_Center;

        }
        public void TransformPointsToGlobal(ref ArrayList points)
        {
            var result = new ArrayList();
            var transMatrix = new Model().GetWorkPlaneHandler().GetCurrentTransformationPlane().TransformationMatrixToGlobal;
            foreach (var cp in points)
            {
                var movedPoint = transMatrix.Transform(cp as Point);
                result.Add(new Point(movedPoint));
            }
            points = result;
        }
        public void TransformPointsToGlobal(ref List<Point> points)
        {
            var result = new List<Point>();
            var transMatrix = new Model().GetWorkPlaneHandler().GetCurrentTransformationPlane().TransformationMatrixToGlobal;
            foreach (var cp in points)
            {
                var movedPoint = transMatrix.Transform(cp as Point);
                result.Add(new Point(movedPoint));
            }
            points = result;
        }

        public void TransformPointsToLocal(ref List<Point> points)
        {
            var result = new List<Point>();
            var transMatrix = new Model().GetWorkPlaneHandler().GetCurrentTransformationPlane().TransformationMatrixToLocal;
            foreach (var cp in points)
            {
                var movedPoint = transMatrix.Transform(cp as Point);
                result.Add(new Point(movedPoint));
            }
            points = result;
        }
        public void TransformPointsToLocal(ref ArrayList points)
        {
            var result = new ArrayList();
            var transMatrix = new Model().GetWorkPlaneHandler().GetCurrentTransformationPlane().TransformationMatrixToLocal;
            foreach (var cp in points)
            {
                var movedPoint = transMatrix.Transform(cp as Point);
                result.Add(new Point(movedPoint));
            }
            points = result;
        }
        public Point getCenterPoint(Point min, Point max)
        {
            double x = min.X + ((max.X - min.X) / 2);
            double y = min.Y + ((max.Y - min.Y) / 2);
            double z = min.Z + ((max.Z - min.Z) / 2);

            return new Point(x, y, z);
        }

        public Point PointsToLocal(Point points)
        {
            Point result = new Point();
            var transMatrix = new Model().GetWorkPlaneHandler().GetCurrentTransformationPlane().TransformationMatrixToLocal;

            result = transMatrix.Transform(points);


            return result;
        }

        public Vector VectorFromPoint(Point p1, Point p2)
        {
            Vector newVector = new Vector(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);

            return newVector;
        }



        public Point PointPlus(Point points, double numberX, double numberY, double numberZ)
        {
            Point newPoint = new Point(points.X + numberX, points.Y + numberY, points.Z + numberZ);

            return newPoint;


        }
        public Point PointPlus(Point points, Vector vec, double number)
        {
            ControlPoint ctrPoint = new ControlPoint(points);
            ctrPoint.Insert();
            var b = Operation.CopyObject(ctrPoint, vec.GetNormal() * number);
            ControlPoint ctrPoint1 = b as ControlPoint;
            Point result = new Point(ctrPoint1.Point);

            ctrPoint.Delete();
            ctrPoint1.Delete();
            return result;

        }

        public Point PointPlus(Point points, Vector vecX, Vector vecY, double numberX, double numberY)
        {
            ControlPoint ctrPoint = new ControlPoint(points);
            ctrPoint.Insert();
            var b = Operation.CopyObject(ctrPoint, vecX.GetNormal() * numberX);
            ControlPoint ctrPoint1 = b as ControlPoint;

            var by = Operation.CopyObject(ctrPoint1, vecY.GetNormal() * numberY);
            ControlPoint ctrPoint2 = by as ControlPoint;
            Point result = new Point(ctrPoint2.Point);
            ctrPoint.Delete();
            ctrPoint1.Delete();
            ctrPoint2.Delete();
            return result;

        }


        public Point PointPlus(Point points, Vector vecX, double numberX, Vector vecY, double numberY)
        {
            ControlPoint ctrPoint = new ControlPoint(points);
            ctrPoint.Insert();
            Operation.MoveObject(ctrPoint, vecX.GetNormal() * numberX);
            Operation.MoveObject(ctrPoint, vecY.GetNormal() * numberY);
            Point result = new Point(ctrPoint.Point);

            ctrPoint.Delete();
            return result;


        }

        public Point PointsToGlobal(Point points)
        {
            Point result = new Point();
            var transMatrix = new Model().GetWorkPlaneHandler().GetCurrentTransformationPlane().TransformationMatrixToGlobal;

            result = transMatrix.Transform(points);


            return result;
        }

        public CoordinateSystem getCsRotate(CoordinateSystem cs, double x, double y, double z)
        {
            var XoayX = MatrixFactory.Rotate(x * Math.PI / 180, cs.AxisX);
            var XoayY = MatrixFactory.Rotate(y * Math.PI / 180, cs.AxisY);
            var XoayZ = MatrixFactory.Rotate(z * Math.PI / 180, Vector.Cross(cs.AxisX, cs.AxisY));
            var Xoay3d = XoayX * XoayY * XoayZ;

            CoordinateSystem newcoor = new CoordinateSystem();

            return new CoordinateSystem
            {
                Origin = cs.Origin,
                AxisX = new Vector(Xoay3d.Transform(new Point(cs.AxisX))),
                AxisY = new Vector(Xoay3d.Transform(new Point(cs.AxisY)))
            };


        }


        public List<Point> addRecTang(Point p, double radius, double numberofsides, double Height)
        {
            Model Model = new Model();
            WorkPlaneHandler whp = Model.GetWorkPlaneHandler();
            TransformationPlane planmodel = whp.GetCurrentTransformationPlane();
            double R = radius / 2;
            double c = numberofsides;
            double a = -360 / c;
            List<t3d.Point> pointLists = new List<t3d.Point>();
            t3d.Point cp2 = new t3d.Point(p.X, p.Y + R, p.Z + Height);
            pointLists.Add(cp2);

            for (int i = 0; i < c - 1; i++)
            {
                CoordinateSystem coor = new CoordinateSystem();
                TransformationPlane rotate = new TransformationPlane(getCsRotate(coor, 0, 0, a));
                whp.SetCurrentTransformationPlane(rotate);
                Model.CommitChanges();
                p = PointsToLocal(p);
                t3d.Point cp1 = new t3d.Point(p.X, p.Y + R, p.Z + Height);
                p = PointsToGlobal(p);
                t3d.Point plist = new t3d.Point(PointsToGlobal(cp1));
                pointLists.Add(plist);


            }

            return pointLists;
        }

        public ArrayList addRecTangPolygon(Point p, double radius, double numberofsides)
        {
            Model Model = new Model();
            WorkPlaneHandler whp = Model.GetWorkPlaneHandler();
            TransformationPlane planmodel = whp.GetCurrentTransformationPlane();
            double R = radius / 2;
            double c = numberofsides;
            double a = -360 / c;
            ArrayList pointLists = new ArrayList();
            t3d.Point cp2 = new t3d.Point(p.X, p.Y + R, p.Z);
            pointLists.Add(cp2);

            for (int i = 0; i < c - 1; i++)
            {
                CoordinateSystem coor = new CoordinateSystem();
                TransformationPlane rotate = new TransformationPlane(getCsRotate(coor, 0, 0, a));
                whp.SetCurrentTransformationPlane(rotate);
                Model.CommitChanges();
                p = PointsToLocal(p);
                t3d.Point cp1 = new t3d.Point(p.X, p.Y + R, p.Z);
                p = PointsToGlobal(p);
                t3d.Point plist = new t3d.Point(PointsToGlobal(cp1));
                pointLists.Add(plist);


            }

            return pointLists;
        }

        public List<Point> getFace(Part cp, int NumberOfFace)
        {
            Solid so = cp.GetSolid();
            var allFaces = new List<Face>();
            List<Point> Points = new List<Point>();
            FaceEnumerator inum = so.GetFaceEnumerator();

            while (inum.MoveNext())
            {
                allFaces.Add(inum.Current as Face);

            }

            Face face = allFaces[NumberOfFace];
            var loops = face.GetLoopEnumerator();
            var faceLoops = new List<Loop>();

            while (loops.MoveNext())
            {
                faceLoops.Add(loops.Current as Loop);
            }

            var firstLoop = faceLoops[0];
            //    if (firstLoop == null) continue;
            VertexEnumerator vertices = firstLoop.GetVertexEnumerator();
            while (vertices.MoveNext())
            {
                var point1 = vertices.Current as Point;
                Points.Add(point1);

            }

            return Points;
        }

        public int getCountFace(Part cp)
        {
            Solid so = cp.GetSolid();
            var allFaces = new List<Face>();
            List<Point> Points = new List<Point>();
            FaceEnumerator inum = so.GetFaceEnumerator();

            while (inum.MoveNext())
            {
                allFaces.Add(inum.Current as Face);

            }

            return allFaces.Count;
        }


        public OBB CreateBox(Part beam, double WidthBoxFormAxitZ)
        {
            OBB obb = null;

            if (beam != null)
            {
                WorkPlaneHandler workPlaneHandler = new Model().GetWorkPlaneHandler();
                TransformationPlane originalTransformationPlane = workPlaneHandler.GetCurrentTransformationPlane();

                Solid solid = beam.GetSolid();
                Point minPointInCurrentPlane = solid.MinimumPoint;
                Point maxPointInCurrentPlane = solid.MaximumPoint;

                Point centerPoint = getCenterPoint(minPointInCurrentPlane, maxPointInCurrentPlane);

                CoordinateSystem coordSys = beam.GetCoordinateSystem();
                TransformationPlane localTransformationPlane = new TransformationPlane(coordSys);
                workPlaneHandler.SetCurrentTransformationPlane(localTransformationPlane);

                solid = beam.GetSolid();
                Point minPoint = solid.MinimumPoint;
                Point maxPoint = solid.MaximumPoint;
                double extent0 = (maxPoint.X - minPoint.X) / 2;
                double extent1 = (maxPoint.Y - minPoint.Y) / 2;
                //double extent2 = (maxPoint.Z - minPoint.Z) / 2;

                //  double extent0 = num;
                // double extent1 = num;
                double extent2 = WidthBoxFormAxitZ;

                workPlaneHandler.SetCurrentTransformationPlane(originalTransformationPlane);

                obb = new OBB(centerPoint, coordSys.AxisX, coordSys.AxisY,
                                coordSys.AxisX.Cross(coordSys.AxisY), extent0, extent1, extent2);
            }

            return obb;
        }

        public OBB CreateBox(Part beam)
        {
            OBB obb = null;

            if (beam != null)
            {
                WorkPlaneHandler workPlaneHandler = new Model().GetWorkPlaneHandler();
                TransformationPlane originalTransformationPlane = workPlaneHandler.GetCurrentTransformationPlane();

                Solid solid = beam.GetSolid();
                Point minPointInCurrentPlane = solid.MinimumPoint;
                Point maxPointInCurrentPlane = solid.MaximumPoint;

                Point centerPoint = getCenterPoint(minPointInCurrentPlane, maxPointInCurrentPlane);

                CoordinateSystem coordSys = beam.GetCoordinateSystem();
                TransformationPlane localTransformationPlane = new TransformationPlane(coordSys);
                workPlaneHandler.SetCurrentTransformationPlane(localTransformationPlane);

                solid = beam.GetSolid();
                Point minPoint = solid.MinimumPoint;
                Point maxPoint = solid.MaximumPoint;
                double extent0 = (maxPoint.X - minPoint.X) / 2;
                double extent1 = (maxPoint.Y - minPoint.Y) / 2;
                double extent2 = (maxPoint.Z - minPoint.Z) / 2;

                workPlaneHandler.SetCurrentTransformationPlane(originalTransformationPlane);

                obb = new OBB(centerPoint, coordSys.AxisX, coordSys.AxisY,
                                coordSys.AxisX.Cross(coordSys.AxisY), extent0, extent1, extent2);
            }

            return obb;
        }

        public BoltArray AddHole(Part part1, t3d.Point FirstPoint, t3d.Point SecondPoint, double boltsize)
        {

            BoltArray B = new BoltArray();

            B.PartToBeBolted = part1;
            B.PartToBoltTo = part1;

            B.FirstPosition = FirstPoint;
            B.SecondPosition = SecondPoint;

            B.BoltSize = boltsize;
            B.Tolerance = 0;
            B.BoltStandard = "8.8XOX";
            B.BoltType = BoltGroup.BoltTypeEnum.BOLT_TYPE_WORKSHOP;
            B.CutLength = 105;

            B.Length = 50;
            B.ExtraLength = 15;
            B.ThreadInMaterial = BoltGroup.BoltThreadInMaterialEnum.THREAD_IN_MATERIAL_YES;

            B.Position.Depth = Position.DepthEnum.MIDDLE;
            B.Position.Plane = Position.PlaneEnum.MIDDLE;
            B.Position.Rotation = Position.RotationEnum.FRONT;

            B.Bolt = false;
            B.Washer1 = true;
            B.Washer2 = true;
            B.Washer3 = true;
            B.Nut1 = true;
            B.Nut2 = true;

            B.Hole1 = false;
            B.Hole2 = true;
            B.Hole3 = true;
            B.Hole4 = true;
            B.Hole5 = true;


            B.AddBoltDistX(0);

            B.AddBoltDistY(0);


            if (!B.Insert())
                Console.WriteLine("BoltXYList Insert failed!");
            return B;
        }
        public double AngleBetweenVectors(Vector vector1, Vector vector2)
        {
            double Angel = Math.Acos(vector1.GetNormal().Dot(vector2.GetNormal()));
            return Angel;
        }

        public double DegreeToRadian(double deg) => Math.PI * deg / 180.0;

        public double RadianToDegree(double rad) => 180.0 * rad / Math.PI;

        public Vector Transform(Matrix matToTrans, Vector vec) => new Vector(new Matrix(matToTrans)
        {
            [3, 0] = 0.0,
            [3, 1] = 0.0,
            [3, 2] = 0.0
        }.Transform((Point)vec));

        public CoordinateSystem Transform(Matrix matToTrans, CoordinateSystem cs)
        {
            Point Origin = matToTrans.Transform(cs.Origin);
            Vector vector1 = Transform(matToTrans, cs.AxisX);
            Vector vector2 = Transform(matToTrans, cs.AxisY);
            Vector AxisX = vector1;
            Vector AxisY = vector2;
            return new CoordinateSystem(Origin, AxisX, AxisY);
        }

        public Plane Transform(Matrix matToTrans, Plane plane) => new Plane()
        {
            Origin = matToTrans.Transform(plane.Origin),
            AxisX = Transform(matToTrans, plane.AxisX),
            AxisY = Transform(matToTrans, plane.AxisY)
        };

        public GeometricPlane Transform(Matrix matToTrans, GeometricPlane plane) => new GeometricPlane()
        {
            Origin = matToTrans.Transform(plane.Origin),
            Normal = Transform(matToTrans, plane.Normal)
        };

        public void DrawCoordinates()
        {
            GraphicsDrawer graphicsDrawer = new GraphicsDrawer();
            graphicsDrawer.DrawLineSegment(new Point(0.0, 0.0, 0.0), new Point(1000.0, 0.0, 0.0), new Color(1.0, 0.0, 0.0));
            graphicsDrawer.DrawLineSegment(new Point(0.0, 0.0, 0.0), new Point(0.0, 3000.0, 0.0), new Color(0.0, 1.0, 0.0));
            graphicsDrawer.DrawLineSegment(new Point(0.0, 0.0, 0.0), new Point(0.0, 0.0, 9000.0), new Color(0.0, 0.0, 1.0));
        }


        //public List<Point> getAllFace(Part part)
        //{
        //    List<Point> MyList = new List<Point>();
        //    ArrayList MyFaceNormalList = new ArrayList();

        //    Solid Solid = part.GetSolid();
        //    FaceEnumerator MyFaceEnum = Solid.GetFaceEnumerator();
        //    while (MyFaceEnum.MoveNext())
        //    {
        //        Face MyFace = MyFaceEnum.Current as Face;
        //        if (MyFace != null)
        //        {
        //            MyFaceNormalList.Add(MyFace.Normal);

        //            LoopEnumerator MyLoopEnum = MyFace.GetLoopEnumerator();
        //            while (MyLoopEnum.MoveNext())
        //            {
        //                Loop MyLoop = MyLoopEnum.Current as Loop;
        //                if (MyLoop != null)
        //                {
        //                    VertexEnumerator MyVertexEnum = MyLoop.GetVertexEnumerator() as VertexEnumerator;
        //                    while (MyVertexEnum.MoveNext())
        //                    {
        //                        Point MyVertex = MyVertexEnum.Current as Point;
        //                        if (MyVertex != null)
        //                        {
        //                            MyList.Add(MyVertex);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return MyList;
        //}

        public void SetComponent(ModelObject M1, ModelObject M2, string ComponetName, int ComponentNumber, string str)
        {
            Component C = new Component();
            ComponentInput CI = new ComponentInput();
            CI.AddInputObject(M1);
            CI.AddInputObject(M2);
            C.Name = ComponetName;
            C.Number = ComponentNumber;
            if (str != "")
            {
                C.LoadAttributesFromFile(str);
            }
            C.SetComponentInput(CI);
            C.Insert();
        }

        public void AddConnection(ModelObject M1, ModelObject M2, string ComponetName,
            int ComponentNumber, bool Cut)
        {
            Connection C = new Connection();
            C.Name = ComponetName;
            C.Number = ComponentNumber;
            // C.LoadAttributesFromFile("standard");

            C.SetPrimaryObject(M1);
            C.SetSecondaryObject(M2);
            //C.SetAttribute("screwdin", "8.8XOX");
            //C.SetAttribute("diameter", 10);
            //C.SetAttribute("tolerance", 2);
            C.SetAttribute("atab5", 1);

            C.Insert();
        }

        public Point GetcenterPoint2Beam(Beam beam1, Beam beam2, bool Center_True_RefLine_False)
        {
            LineSegment Giao = new LineSegment();
            if (Center_True_RefLine_False)
            {
                Line line1 = new Line(beam1.GetCenterLine(false)[0] as Point, beam1.GetCenterLine(false)[1] as Point);
                Line line2 = new Line(beam2.GetCenterLine(false)[0] as Point, beam2.GetCenterLine(false)[1] as Point);
                Giao = Intersection.LineToLine(line1, line2);
            }
            else
            {
                Line line1 = new Line(beam1.GetReferenceLine(false)[0] as Point, beam1.GetReferenceLine(false)[1] as Point);
                Line line2 = new Line(beam2.GetReferenceLine(false)[0] as Point, beam2.GetReferenceLine(false)[1] as Point);
                Giao = Intersection.LineToLine(line1, line2);
            }


            Point pointGiao = getCenterPoint(Giao.Point1, Giao.Point2);
            return pointGiao;
        }

        public Point GetcenterPoint2Line(Line line1, Line line2)
        {
            LineSegment Giao = Intersection.LineToLine(line1, line2);
            Point pointGiao = getCenterPoint(Giao.Point1, Giao.Point2);
            return pointGiao;
        }

        public bool VectorIsCoDirectional(Vector Vector1, Vector Vector2)
            => t3d.Parallel.VectorToVector(Vector1, Vector2)
            && Compare.IsEqual(Vector1.GetNormal().Dot(Vector2.GetNormal()), 1.0, 0.0001);


    }
    public static class Compare
    {
        public static bool IsEqual(double val1, double val2) => Math.Abs(val1 - val2) < 0.0001;

        public static bool IsEqual(double val1, double val2, double eps) => Math.Abs(val1 - val2) < eps;

        public static bool IsEqual(Point point1, Point point2) => Compare.IsEqual(point1.X, point2.X) && Compare.IsEqual(point1.Y, point2.Y) && Compare.IsEqual(point1.Z, point2.Z);

        public static bool IsGreaterThan(double greaterValue, double lesserValue) => greaterValue - lesserValue > 0.0001;

        public static bool IsLessThan(double lesserValue, double greaterValue) => greaterValue - lesserValue > 0.0001;

        public static bool IsGreaterThanOrEqual(double greaterValue, double lesserValue) => Compare.IsGreaterThan(greaterValue, lesserValue) || Compare.IsEqual(greaterValue, lesserValue);

        public static bool IsLessThanOrEqual(double lesserValue, double greaterValue) => Compare.IsLessThan(lesserValue, greaterValue) || Compare.IsEqual(greaterValue, lesserValue);
    }



}
