
using System;
using System.Collections.Generic;
using System.Linq;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

using Plasmoid.Extensions;

namespace DrawingShared
{

    public partial class DrawingView 
    {

        Rectangle pathRect1 = new Rectangle(50, 50, 100, 100);
        RectangleF pathRectF1 = new RectangleF(110, 60, 100, 100);
        Rectangle pathRect2 = new Rectangle(110, 60, 100, 100);
        RectangleF pathRectF2 = new RectangleF(110, 60, 100, 100);
        Rectangle pathRect3 = new Rectangle(50, 50, 50, 100);
        RectangleF pathRectF3 = new RectangleF(50, 50, 50, 100);
        Rectangle pathRect4 = new Rectangle(110, 60, 100, 50);
        RectangleF pathRectF4 = new RectangleF(110, 60, 100, 50);


        void PlatformInitialize()
        {

            // Load our painting view methods.
            paintViewActions = new Action<Graphics>[]
                {
                      AddArcRectangle,
                      AddArcRectangleF,
                      AddArc1,
                      AddArc2,
                      AddBezier1,
                      AddBezier2,
                      AddBezier3,
                      AddBezier4,
                      AddBeziers1,
                      AddBeziers2,
                      AddClosedCurve1,
                      AddClosedCurve2,
                      AddCurve1,
                      AddCurve2,
                      AddEllipse1,
                      AddEllipse2,
                      AddEllipse3,
                      AddEllipse4,
                      AddLine1,
                      AddLine2,
                      AddLine3,
                      AddLine4,
                      AddLines1,
                      AddLines2,
                      AddPie1,
                      AddPie2,
                      AddPie3,
                      AddPolygon1,
                      AddPolygon2,
                      AddRectangle1,
                      AddRectangle2,
                      AddRectangles1,
                      AddRectangles2,
                      AddPath1,
                      CloseAllFigures1,
                      GetLastPoint1,
                      GetLastPoint2,
                      Reset1,
                      Reverse1,
                      SetMarkers1,
                      SetMarkers2,
                      ClearMarkers1,
                      ClearMarkers2,
                      TransformPath,
                      StartFigure,
                      GetBounds,
                      Flatten1,
                      PathIterator1,
                      PathIterator2,
                      PathIterator3,
                      PathIterator4,
                      PathIterator5,
                      PathIterator6,
                      PathIterator7,
                      Widen1,
                      Widen2,
                      RoundeRectangle1,
                      AddString1,
                      AddString2,
                      AddString3,
                      AddString4,
                      IsVisible1,
                      IsVisible2,
                      SetClip1,
                      SetClip2,
                      SetClip3,
                      SetClip4,
                      SetClip5,
                      IsOutlineVisible1,
                      Warp1,
                      Warp2,
                      Warp3,
                      Warp4,

 
                };
        }

        protected void OnPaint(PaintEventArgs e)
        {
            Graphics g = Graphics.FromCurrentContext();
            g.InterpolationMode = InterpolationMode.NearestNeighbor;

            paintViewActions[currentView].Invoke(g);
            if (saveCurrentView)
                SavePaintView(paintViewActions[currentView]);

            g.ResetTransform ();
            Brush sBrush = Brushes.Black;

            if (!g.IsClipEmpty) 
            {
                var clipPoint = PointF.Empty;
                var clipString = string.Format("Clip-{0}", g.ClipBounds);
                g.ResetClip ();
                var clipSize = g.MeasureString(clipString, clipFont);
                clipPoint.X = (ClientRectangle.Width / 2) - (clipSize.Width / 2);
                clipPoint.Y = 5;
                g.DrawString(clipString, clipFont, sBrush, clipPoint );
            }

            var anyKeyPoint = PointF.Empty;

            #if __MAC__
            var anyKey = "Press any key to continue.";
            #endif
            #if __IOS__
            var anyKey = "Tap Screen to continue.";
            #endif

            var anyKeySize = g.MeasureString(anyKey, anyKeyFont);
            anyKeyPoint.X = (ClientRectangle.Width / 2) - (anyKeySize.Width / 2);
            anyKeyPoint.Y = ClientRectangle.Height - (anyKeySize.Height + 10);
            g.DrawString(anyKey, anyKeyFont, sBrush, anyKeyPoint );

            var title = paintViewActions[currentView].Method.Name;
            anyKeySize = g.MeasureString(title, anyKeyFont);
            anyKeyPoint.X = (ClientRectangle.Width / 2) - (anyKeySize.Width / 2);
            anyKeyPoint.Y -= anyKeySize.Height;
            g.DrawString(title, anyKeyFont, sBrush, anyKeyPoint );

            g.Dispose();

        }

		private void AddArcRectangle (Graphics g)
		{

			// Create a GraphicsPath object.
			GraphicsPath myPath = new GraphicsPath();

			// Set up and call AddArc, and close the figure.
			myPath.StartFigure();
			myPath.AddArc(pathRect3, 0, 180);
			myPath.CloseFigure();

			// Draw the path to screen.
			g.DrawPath(new Pen(Color.Red, 3), myPath);

		}

		private void AddArcRectangleF (Graphics g)
		{

			// Create a GraphicsPath object.
			GraphicsPath myPath = new GraphicsPath();

			// Set up and call AddArc, and close the figure.
			myPath.StartFigure();
			myPath.AddArc(pathRectF3, 0, 180);
			myPath.CloseFigure();

			// Draw the path to screen.
			g.DrawPath(new Pen(Color.Red, 3), myPath);


		}

		private void AddArc1 (Graphics g)
		{

			// Create a GraphicsPath object.
			GraphicsPath myPath = new GraphicsPath();

			// Set up and call AddArc, and close the figure.
			myPath.StartFigure();
			myPath.AddArc(110,60,50,100, 0, 180);
			myPath.CloseFigure();

			// Draw the path to screen.
			g.DrawPath(new Pen(Color.Red, 3), myPath);

			
		}

		private void AddArc2 (Graphics g)
		{

			// Create a GraphicsPath object.
			GraphicsPath myPath = new GraphicsPath();

			// Set up and call AddArc, and close the figure.
			myPath.StartFigure();
			myPath.AddArc(110.5f,60.5f,50.5f,100.5f, 0, 180);
			myPath.CloseFigure();

			// Draw the path to screen.
			g.DrawPath(new Pen(Color.Red, 3), myPath);

			
		}

		private void AddBezier1(Graphics g)
		{

			// Create a new Path.
			GraphicsPath myPath = new GraphicsPath();

			// Call AddBezier.
			myPath.StartFigure();
			myPath.AddBezier(50, 50, 70, 0, 100, 120, 150, 50);

			// Close the curve.
			myPath.CloseFigure();

			// Draw the path to screen.
			g.DrawPath(new Pen(Color.Red, 2), myPath);

			
		}

		private void AddBezier2(Graphics g)
		{

			// Create a new Path.
			GraphicsPath myPath = new GraphicsPath();

			// Call AddBezier.
			myPath.StartFigure();
			myPath.AddBezier(new Point(50, 50), new Point(70, 0), new Point(100, 120), new Point(150, 50));

			// Close the curve.
			myPath.CloseFigure();

			// Draw the path to screen.
			g.DrawPath(new Pen(Color.Red, 2), myPath);

			
		}

		private void AddBezier3(Graphics g)
		{

			// Create a new Path.
			GraphicsPath myPath = new GraphicsPath();

			// Call AddBezier.
			myPath.StartFigure();
			myPath.AddBezier(new PointF(50.5f, 50.5f), new PointF(70.5f, 0), new PointF(100.5f, 120.5f), new PointF(150.5f, 50.5f));

			// Close the curve.
			myPath.CloseFigure();

			// Draw the path to screen.
			g.DrawPath(new Pen(Color.Red, 2), myPath);

			
		}

		private void AddBezier4(Graphics g)
		{

			// Create a new Path.
			GraphicsPath myPath = new GraphicsPath();

			// Call AddBezier.
			myPath.StartFigure();
			myPath.AddBezier(50.5f, 50.5f, 70.5f, 0, 100.5f, 120.5f, 150.5f, 50.5f);

			// Close the curve.
			myPath.CloseFigure();

			// Draw the path to screen.
			g.DrawPath(new Pen(Color.Red, 2), myPath);

			
		}

		private void AddBeziers1(Graphics g)
		{

			// Adds two Bezier curves.
			Point[] myArray =
			{
				new Point(20, 100),
				new Point(40, 75),
				new Point(60, 125),
				new Point(80, 100),
				new Point(100, 50),
				new Point(120, 150),
				new Point(140, 100)
			};

			// Create the path and add the curves.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddBeziers(myArray);

			// Draw the path to the screen.
			Pen myPen = new Pen(Color.Black, 2);
			g.DrawPath(myPen, myPath);

			
		}

		private void AddBeziers2(Graphics g)
		{

			// Adds two Bezier curves.
			PointF[] myArray =
			{
				new PointF(20, 100),
				new PointF(40, 75),
				new PointF(60, 125),
				new PointF(80, 100),
				new PointF(100, 50),
				new PointF(120, 150),
				new PointF(140, 100)
			};

			// Create the path and add the curves.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddBeziers(myArray);

			// Draw the path to the screen.
			Pen myPen = new Pen(Color.Black, 2);
			g.DrawPath(myPen, myPath);

			
		}

		
		private void AddClosedCurve1(Graphics g)
		{

			// Creates a symetrical, closed curve.
			Point[] myArray =
			{
				new Point(20,100),
				new Point(40,150),
				new Point(60,125),
				new Point(40,100),
				new Point(60,75),
				new Point(40,50)
			};

			// Create a new path and add curve.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddClosedCurve(myArray, 0.5f);
			Pen myPen = new Pen(Color.Black, 2);

			// Draw the path to screen.
			g.DrawPath(myPen, myPath);

			
		}

		private void AddClosedCurve2(Graphics g)
		{

			// Creates a symetrical, closed curve.
			PointF[] myArray =
			{
				new PointF(20,100),
				new PointF(40,150),
				new PointF(60,125),
				new PointF(40,100),
				new PointF(60,75),
				new PointF(40,50)
			};

			// Create a new path and add curve.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddClosedCurve(myArray);
			Pen myPen = new Pen(Color.Black, 2);

			// Draw the path to screen.
			g.DrawPath(myPen, myPath);

			
		}

		private void AddCurve1(Graphics g)
		{

			// Create some points.
			Point point1 = new Point(20, 20);
			Point point2 = new Point(40, 0);
			Point point3 = new Point(60, 40);
			Point point4 = new Point(80, 20);

			// Create an array of the points.
			Point[] curvePoints = {point1, point2, point3, point4};

			// Create a GraphicsPath object and add a curve.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddCurve(curvePoints, 0, 3, 0.8f);

			// Draw the path to the screen.
			Pen myPen = new Pen(Color.Black, 2);
			g.DrawPath(myPen, myPath);

			
		}

		private void AddCurve2(Graphics g)
		{

			// Create some points.
			PointF point1 = new PointF(20, 20);
			PointF point2 = new PointF(40, 0);
			PointF point3 = new PointF(60, 40);
			PointF point4 = new PointF(80, 20);

			// Create an array of the points.
			PointF[] curvePoints = {point1, point2, point3, point4};

			// Create a GraphicsPath object and add a curve.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddCurve(curvePoints, 0.8f);

			// Draw the path to the screen.
			Pen myPen = new Pen(Color.Black, 2);
			g.DrawPath(myPen, myPath);

			
		}

		private void AddEllipse1(Graphics g)
		{

			// Create a path and add an ellipse.
			Rectangle myEllipse = new Rectangle(20, 20, 100, 50);
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddEllipse(myEllipse);

			// Draw the path to the screen.
			Pen myPen = new Pen(Color.Black, 2);
			g.DrawPath(myPen, myPath);
			
		}

		private void AddEllipse2(Graphics g)
		{

			// Create a path and add an ellipse.
			RectangleF myEllipse = new RectangleF(20.5f, 20.5f, 100.5f, 50.5f);
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddEllipse(myEllipse);

			// Draw the path to the screen.
			Pen myPen = new Pen(Color.Black, 2);
			g.DrawPath(myPen, myPath);
			
		}

		private void AddEllipse3(Graphics g)
		{

			// Create a path and add an ellipse.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddEllipse(20, 20, 100, 50);

			// Draw the path to the screen.
			Pen myPen = new Pen(Color.Black, 2);
			g.DrawPath(myPen, myPath);
			
		}

		
		private void AddEllipse4(Graphics g)
		{

			// Create a path and add an ellipse.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddEllipse(20.5f, 20.5f, 100.5f, 50.5f);

			// Draw the path to the screen.
			Pen myPen = new Pen(Color.Black, 2);
			g.DrawPath(myPen, myPath);
			
		}

		private void AddLine1(Graphics g)
		{

			//Create a path and add a symetrical triangle using AddLine.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddLine(30, 30, 60, 60);
			myPath.AddLine(60, 60, 0, 60);
			myPath.AddLine(0, 60, 30, 30);

			// Draw the path to the screen.
			Pen myPen = new Pen(Color.Black, 2);
			g.DrawPath(myPen, myPath);

			
		}

		private void AddLine2(Graphics g)
		{

			//Create a path and add a symetrical triangle using AddLine.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddLine(30.5f, 30.5f, 60.5f, 60.5f);
			myPath.AddLine(60.5f, 60.5f, 0, 60.5f);
			myPath.AddLine(0, 60.5f, 30.5f, 30.5f);

			// Draw the path to the screen.
			Pen myPen = new Pen(Color.Black, 2);
			g.DrawPath(myPen, myPath);

			
		}

		private void AddLine3(Graphics g)
		{

			//Create a path and add a symetrical triangle using AddLine.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddLine(new Point(30, 30), new Point(60, 60));
			myPath.AddLine(new Point(60, 60), new Point(0, 60));
			myPath.AddLine(new Point(0, 60), new Point(30, 30));

			// Draw the path to the screen.
			Pen myPen = new Pen(Color.Black, 2);
			g.DrawPath(myPen, myPath);
			
			
		}

		private void AddLine4(Graphics g)
		{

			//Create a path and add a symetrical triangle using AddLine.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddLine(new PointF(30.5f, 30.5f), new PointF(60.5f, 60.5f));
			myPath.AddLine(new PointF(60.5f, 60.5f), new PointF(0, 60.5f));
			myPath.AddLine(new PointF(0, 60.5f), new PointF(30.5f, 30.5f));

			// Draw the path to the screen.
			Pen myPen = new Pen(Color.Black, 2);
			g.DrawPath(myPen, myPath);
			
		}
		
		private void AddLines1(Graphics g)
		{

			
			// Create a symetrical triangle using an array of points.
			Point[] myArray =
			{
				new Point(30,30),
				new Point(60,60),
				new Point(0,60),
				new Point(30,30)
			};

			//Create a path and add lines.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddLines(myArray);

			// Draw the path to the screen.
			Pen myPen = new Pen(Color.Black, 2);
			g.DrawPath(myPen, myPath);

			
		}
		
		private void AddLines2(Graphics g)
		{


			// Create a symetrical triangle using an array of points.
			PointF[] myArray =
			{
				new PointF(30,30),
				new PointF(60,60),
				new PointF(0,60),
				new PointF(30,30)
			};

			//Create a path and add lines.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddLines(myArray);

			// Draw the path to the screen.
			Pen myPen = new Pen(Color.Black, 2);
			g.DrawPath(myPen, myPath);

			
		}

		private void AddPie1(Graphics g)
		{

			// Create a pie slice of a circle using the AddPie method.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddPie(20, 20, 70, 70, -45, 90);

			// Draw the path to the screen.
			Pen myPen = new Pen(Color.Black, 2);
			g.DrawPath(myPen, myPath);

			
		}

		private void AddPie2(Graphics g)
		{

			// Create a pie slice of a circle using the AddPie method.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddPie(20.5f, 20.5f, 70.5f, 70.5f, -45, 90);

			// Draw the path to the screen.
			Pen myPen = new Pen(Color.Black, 2);
			g.DrawPath(myPen, myPath);
			
			
		}

		private void AddPie3(Graphics g)
		{

			// Create a pie slice of a circle using the AddPie method.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddPie(new Rectangle(20, 20, 70, 70), -45, 90);

			// Draw the path to the screen.
			Pen myPen = new Pen(Color.Black, 2);
			g.DrawPath(myPen, myPath);

			
		}

		private void AddPolygon1(Graphics g)
		{

			// Create an array of points.
			Point[] myArray =
			{
				new Point(23, 20),
				new Point(40, 10),
				new Point(57, 20),
				new Point(50, 40),
				new Point(30, 40)
			};

			// Create a GraphicsPath object and add a polygon.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddPolygon(myArray);

			// Draw the path to the screen.
			Pen myPen = new Pen(Color.Black, 2);
			g.DrawPath(myPen, myPath);

			
		}	
		
		private void AddPolygon2(Graphics g)
		{

			// Create an array of points.
			PointF[] myArray =
			{
				new PointF(23, 20),
				new PointF(40, 10),
				new PointF(57, 20),
				new PointF(50, 40),
				new PointF(30, 40)
			};

			// Create a GraphicsPath object and add a polygon.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddPolygon(myArray);

			// Draw the path to the screen.
			Pen myPen = new Pen(Color.Black, 2);
			g.DrawPath(myPen, myPath);

			
		}		
	
		private void AddRectangle1(Graphics g)
		{

			// Create a GraphicsPath object and add a rectangle to it.
			GraphicsPath myPath = new GraphicsPath();
			Rectangle pathRect = new Rectangle(20, 20, 100, 200);
			myPath.AddRectangle(pathRect);

			// Draw the path to the screen.
			Pen myPen = new Pen(Color.Black, 2);
			g.DrawPath(myPen, myPath);
			
			
		}
		
		private void AddRectangle2(Graphics g)
		{

			// Create a GraphicsPath object and add a rectangle to it.
			GraphicsPath myPath = new GraphicsPath();
			RectangleF pathRect = new RectangleF(20, 20, 100, 200);
			myPath.AddRectangle(pathRect);

			// Draw the path to the screen.
			Pen myPen = new Pen(Color.Black, 2);
			g.DrawPath(myPen, myPath);

			
		}

		private void AddRectangles1(Graphics g)
		{

			// Adds a pattern of rectangles to a GraphicsPath object.
			GraphicsPath myPath = new GraphicsPath();
			Rectangle[] pathRects =
			{
				new Rectangle(20,20,100,200),
				new Rectangle(40,40,120,220),
				new Rectangle(60,60,240,140)
			};
			myPath.AddRectangles(pathRects);

			// Draw the path to the screen.
			Pen myPen = new Pen(Color.Black, 2);
			g.DrawPath(myPen, myPath);

			

		}

		private void AddRectangles2(Graphics g)
		{

			// Adds a pattern of rectangles to a GraphicsPath object.
			GraphicsPath myPath = new GraphicsPath();
			RectangleF[] pathRects =
			{
				new RectangleF(20,20,100,200),
				new RectangleF(40,40,120,220),
				new RectangleF(60,60,240,140)
			};
			myPath.AddRectangles(pathRects);

			// Draw the path to the screen.
			Pen myPen = new Pen(Color.Black, 2);
			g.DrawPath(myPen, myPath);

			

		}

		private void AddPath1(Graphics g)
		{

			// Create the first pathright side up triangle.
			Point[] myArray =
			{
				new Point(30,30),
				new Point(60,60),
				new Point(0,60),
				new Point(30,30)
			};
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddLines(myArray);

			// Create the second pathinverted triangle.
			Point[] myArray2 =
			{
				new Point(30,30),
				new Point(0,0),
				new Point(60,0),
				new Point(30,30)
			};
			GraphicsPath myPath2 = new GraphicsPath();
			myPath2.AddLines(myArray2);

			// Add the second path to the first path.
			myPath.AddPath(myPath2,true);

			// Draw the combined path to the screen.
			Pen myPen = new Pen(Color.Black, 2);
			g.DrawPath(myPen, myPath);

			
		}

		private void CloseAllFigures1(Graphics g)
		{

			// Create a path containing several open-ended figures.
			GraphicsPath myPath = new GraphicsPath();
			myPath.StartFigure();
			myPath.AddLine(new Point(10, 10), new Point(150, 10));
			myPath.AddLine(new Point(150, 10), new Point(10, 150));
			myPath.StartFigure();
			myPath.AddArc(200, 200, 100, 100, 0, 90);
			myPath.StartFigure();
			Point point1 = new Point(300, 300);
			Point point2 = new Point(400, 325);
			Point point3 = new Point(400, 375);
			Point point4 = new Point(300, 400);
			Point[] points = {point1, point2, point3, point4};
			myPath.AddCurve(points);

			// Close all the figures.
			myPath.CloseAllFigures();

			// Draw the path to the screen.
			g.DrawPath(new Pen(Color.Black, 3), myPath);

			
		}

		private void GetLastPoint1(Graphics g)
		{
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddLine(100, 100, 300, 100);
			PointF lastPoint = myPath.GetLastPoint();
			if(!lastPoint.IsEmpty)
			{
				string lastPointXString = lastPoint.X.ToString();
				string lastPointYString = lastPoint.Y.ToString();
				Console.WriteLine(lastPointXString + ", " + lastPointYString);
			}
			else
				Console.WriteLine("lastPoint is empty");

			// Draw the path to the screen.
			g.DrawPath(new Pen(Color.Black, 2), myPath);

			
		}

		
		private void GetLastPoint2(Graphics g)
		{
			GraphicsPath myPath = new GraphicsPath();

			try
			{
				PointF lastPoint = myPath.GetLastPoint();
				if(!lastPoint.IsEmpty)
				{
					string lastPointXString = lastPoint.X.ToString();
					string lastPointYString = lastPoint.Y.ToString();
					Console.WriteLine(lastPointXString + ", " + lastPointYString);
				}
				else
					Console.WriteLine("lastPoint is empty");
			}
			catch (ArgumentException ae) 
			{
				Console.WriteLine (ae.Message);
			}

			// Draw the path to the screen.
			g.DrawPath(new Pen(Color.Black, 2), myPath);

			
		}

		public void Reset1(Graphics g)
		{
			Font myFont = new Font("Arial", 8);

			// Create a path and add a line, an ellipse, and an arc.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddLine(new Point(0, 0), new Point(100, 100));
			myPath.AddEllipse(100, 100, 200, 250);
			myPath.AddArc(300, 250, 100, 100, 0, 90);

			// Draw the pre-reset path to the screen
			g.DrawPath (Pens.Blue, myPath);

			// Draw the pre-reset points array to the screen.
			DrawPoints1(g, myPath.PathPoints, 20);

			// Reset the path.
			myPath.Reset();

			// See if any points remain. 
			if(myPath.PointCount > 0)
			{

				// Draw the post-reset points array to the screen.
				DrawPoints1(g, myPath.PathPoints, 150);
			}
			else 

				// If there are no points, say so.
				g.DrawString("No Points",
				                      myFont,
				                      Brushes.Black,
				                      150,
				                      20);

			
		} 
		//End GraphicsPathResetExample 

		// A helper function GraphicsPathResetExample uses to draw the points. 

		// to the screen. 
		public void DrawPoints1(Graphics g, PointF[] pathPoints, int xOffset)
		{
			int y = 20;
			Font myFont = new Font("Arial", 8);
			for(int i=0;i < pathPoints.Length; i++)
			{
				g.DrawString(pathPoints[i].X.ToString() + ", " +
				                      pathPoints[i].Y.ToString(),
				                      myFont,
				                      Brushes.Black,
				                      xOffset,
				                      y);
				y += 20;
			}
		} 
		// End DrawPoints

		public void Reverse1(Graphics g)
		{

			// Create a path and add a line, ellipse, and arc.
			GraphicsPath myPath = new GraphicsPath(FillMode.Winding);
			myPath.AddLine(new Point(0, 0), new Point(100, 100));
			myPath.AddEllipse(100, 100, 200, 250);
			myPath.AddArc(300, 250, 100, 100, 0, 90);

			g.DrawPath (Pens.Red, myPath);
			g.FillPath (Brushes.Red, myPath);

			var pathPoints = myPath.PathPoints;
			var pathTypes = myPath.PathTypes;

			Console.WriteLine ("Before reverse");
			for(int i=0;i < myPath.PathTypes.Length; i++)
			{
				Console.WriteLine ("{0} - {1},{2}", (PathPointType)pathTypes [i], pathPoints[i].X,pathPoints[i].Y);
			}

			// Draw the first set of points to the screen.
			DrawPoints2(g, myPath.PathPoints, 20);

			// Call GraphicsPath.Reverse.
			myPath.Reverse();

			g.DrawPath (Pens.Blue, myPath);
			g.FillPath (Brushes.Blue, myPath);

			pathPoints = myPath.PathPoints;
			pathTypes = myPath.PathTypes;


			Console.WriteLine ("After reverse");
			for(int i=0;i < myPath.PathTypes.Length; i++)
			{
				Console.WriteLine ("{0} - {1},{2}", (PathPointType)pathTypes [i], pathPoints[i].X,pathPoints[i].Y);
			}

			// Draw the reversed set of points to the screen.
			DrawPoints2(g, myPath.PathPoints, 150);

			
		}
		//End GraphicsPathReverseExample. 

		// A helper function GraphicsPathReverseExample is used to draw the 

		// points to the screen. 
		public void DrawPoints2(Graphics g, PointF[] pathPoints, int xOffset)
		{
			int y = 20;
			Font myFont = new Font("Arial", 8);
			for(int i=0;i < pathPoints.Length; i++)
			{
				g.DrawString(pathPoints[i].X.ToString() + ", " +
				                      pathPoints[i].Y.ToString(),
				                      myFont,
				                      Brushes.Black,
				                      xOffset,
				                      y);
				y += 20;
			}
		} 
		// End DrawPoints

		private void SetMarkers1(Graphics g)
		{

			// Create a path and set two markers.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddLine(new Point(0, 0), new Point(50, 50));
			myPath.SetMarkers();
			Rectangle rect = new Rectangle(50, 50, 50, 50);
			myPath.AddRectangle(rect);
			myPath.SetMarkers();
			myPath.AddEllipse(100, 100, 100, 50);

			var pathPoints = myPath.PathPoints;
			var pathTypes = myPath.PathTypes;

			// Draw the path to screen.
			g.DrawPath(new Pen(Color.Black, 2), myPath);


			
		}

		private void SetMarkers2(Graphics g)
		{

			// Create a path and set two markers.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddLine(new Point(0, 0), new Point(50, 50));
			myPath.SetMarkers();
			Rectangle rect = new Rectangle(50, 50, 50, 50);
			myPath.AddRectangle(rect);
			myPath.SetMarkers();
			myPath.AddEllipse(100, 100, 100, 50);

			var pathPoints = myPath.PathPoints;
			var pathTypes = myPath.PathTypes;

			Console.WriteLine("SetMarkers Before reverse");
			for (int i = 0; i < myPath.PathTypes.Length; i++)
			{
				Console.WriteLine("{0} - {1},{2}", (PathPointType)pathTypes[i], pathPoints[i].X, pathPoints[i].Y);
			}

			// Draw the path to screen.
			g.FillPath(Brushes.Red, myPath);
			g.DrawPath(new Pen(Color.Black, 2), myPath);

			// Draw the first set of points to the screen.
			DrawPoints2(g, myPath.PathPoints, 20);

			// Call GraphicsPath.Reverse.
			myPath.Reverse();

			pathPoints = myPath.PathPoints;
			pathTypes = myPath.PathTypes;


			Console.WriteLine("SetMarkers After reverse");
			for (int i = 0; i < myPath.PathTypes.Length; i++)
			{
				Console.WriteLine("{0} - {1},{2}", (PathPointType)pathTypes[i], pathPoints[i].X, pathPoints[i].Y);
			}

			// Draw the path to screen.
			g.FillPath(Brushes.CornflowerBlue, myPath);
			g.DrawPath(new Pen(Color.Black, 2), myPath);


			// Draw the reversed set of points to the screen.
			DrawPoints2(g, myPath.PathPoints, 150);

			
		}

		
		private void ClearMarkers1(Graphics g)
		{

			// Create a path and set two markers.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddLine(new Point(0, 0), new Point(50, 50));
			myPath.SetMarkers();
			Rectangle rect = new Rectangle(50, 50, 50, 50);
			myPath.AddRectangle(rect);
			myPath.SetMarkers();
			myPath.AddEllipse(100, 100, 100, 50);

			myPath.ClearMarkers ();

			// Draw the path to screen.
			g.DrawPath(new Pen(Color.Black, 2), myPath);


			
		}

		private void ClearMarkers2(Graphics g)
		{

			// Create a path and set two markers.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddLine(new Point(0, 0), new Point(50, 50));
			myPath.SetMarkers();
			Rectangle rect = new Rectangle(50, 50, 50, 50);
			myPath.AddRectangle(rect);
			myPath.SetMarkers();
			myPath.AddEllipse(100, 100, 100, 50);

			var pathPoints = myPath.PathPoints;
			var pathTypes = myPath.PathTypes;

			Console.WriteLine("ClearMarkers Before reverse");
			for (int i = 0; i < myPath.PathTypes.Length; i++)
			{
				Console.WriteLine("{0} - {1},{2}", (PathPointType)pathTypes[i], pathPoints[i].X, pathPoints[i].Y);
			}

			// Draw the path to screen.
			g.FillPath(Brushes.Red, myPath);
			g.DrawPath(new Pen(Color.Black, 2), myPath);

			// Draw the first set of points to the screen.
			DrawPoints2(g, myPath.PathPoints, 20);

			// Call GraphicsPath.Reverse.
			myPath.Reverse();

			pathPoints = myPath.PathPoints;
			pathTypes = myPath.PathTypes;


			Console.WriteLine("ClearMarkers After reverse");
			for (int i = 0; i < myPath.PathTypes.Length; i++)
			{
				Console.WriteLine("{0} - {1},{2}", (PathPointType)pathTypes[i], pathPoints[i].X, pathPoints[i].Y);
			}

			// Call GraphicsPath.ClearMarkers.
			myPath.ClearMarkers();

			pathPoints = myPath.PathPoints;
			pathTypes = myPath.PathTypes;


			Console.WriteLine("ClearMarkers After Clear");
			for (int i = 0; i < myPath.PathTypes.Length; i++)
			{
				Console.WriteLine("{0} - {1},{2}", (PathPointType)pathTypes[i], pathPoints[i].X, pathPoints[i].Y);
			}
			// Draw the path to screen.
			g.FillPath(Brushes.CornflowerBlue, myPath);
			g.DrawPath(new Pen(Color.Black, 2), myPath);


			// Draw the reversed set of points to the screen.
			DrawPoints2(g, myPath.PathPoints, 150);

			
		}

		private void TransformPath(Graphics g)
		{

			// Create a path and add and ellipse.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddEllipse(50, 50, 100, 200);

			// Draw the starting position to screen.
			g.DrawPath(Pens.Black, myPath);

			// Move the ellipse 100 points to the right.
			Matrix translateMatrix = new Matrix();
			translateMatrix.Translate(100, 0);
			myPath.Transform(translateMatrix);

			// Draw the transformed ellipse to the screen.
			g.DrawPath(new Pen(Color.Red, 2), myPath);

			

		}

		public void StartFigure(Graphics g)
		{

			// Create a GraphicsPath object.
			GraphicsPath myPath = new GraphicsPath();

			// First set of figures.
			myPath.StartFigure();
			myPath.AddArc(10, 10, 50, 50, 0, 270);
			myPath.AddLine(new Point(50, 0), new Point(100, 50));
			myPath.AddArc(50, 100, 75, 75, 0, 270);
			myPath.CloseFigure();
			myPath.StartFigure();
			myPath.AddArc(100, 10, 50, 50, 0, 270);

			// Second set of figures.
			myPath.StartFigure();
			myPath.AddArc(10, 200, 50, 50, 0, 270);
			myPath.CloseFigure();
			myPath.StartFigure();
			myPath.AddLine(new Point(60, 200), new Point(110, 250));
			myPath.AddArc(50, 300, 75, 75, 0, 270);
			myPath.CloseFigure();
			myPath.StartFigure();
			myPath.AddArc(100, 200, 50, 50, 0, 270);

			// Draw the path to the screen.
			g.DrawPath(new Pen(Color.Black), myPath);

			
		} 

		public void GetBounds(Graphics g)
		{

			// Create path number 1 and a Pen for drawing.
			GraphicsPath myPath = new GraphicsPath();
			Pen pathPen = new Pen(Color.Black, 1);

			// Add an Ellipse to the path and Draw it (circle in start 

			// position).
			myPath.AddEllipse(20, 20, 100, 100);
			g.DrawPath(pathPen, myPath);

			// Get the path bounds for Path number 1 and draw them.
			RectangleF boundRect = myPath.GetBounds();
			g.DrawRectangle(new Pen(Color.Red, 1),
			                         boundRect.X,
			                         boundRect.Y,
			                         boundRect.Height,
			                         boundRect.Width);

			// Create a second graphics path and a wider Pen.
			GraphicsPath myPath2 = new GraphicsPath();
			Pen pathPen2 = new Pen(Color.Black, 10);

			// Create a new ellipse with a width of 10.
			myPath2.AddEllipse(150, 20, 100, 100);
			myPath2.Widen(pathPen2);
			g.FillPath(Brushes.Black, myPath2);

			// Get the second path bounds.
			RectangleF boundRect2 = myPath2.GetBounds();

			// Draw the bounding rectangle.
			g.DrawRectangle(new Pen(Color.Red, 1),
			                         boundRect2.X,
			                         boundRect2.Y,
			                         boundRect2.Height,
			                         boundRect2.Width);

			
		}

		private void Flatten1 (Graphics g)
		{
			GraphicsPath myPath = new GraphicsPath();
			Matrix translateMatrix = new Matrix();
			translateMatrix.Translate(0, 10);
			Point point1 = new Point(20, 120);
			Point point2 = new Point(70, 30);
			Point point3 = new Point(130, 220);
			Point point4 = new Point(180, 120);
			Point[] points = {point1, point2, point3, point4};
			myPath.AddCurve(points);

			g.DrawPath(new Pen(Color.Black, 1), myPath);

			var pathPoints = myPath.PathPoints;
			var pathTypes = myPath.PathTypes;

			Console.WriteLine("Flatten before Flattening");
			for (int i = 0; i < myPath.PathTypes.Length; i++)
			{
				Console.WriteLine("{0} - {1},{2}", (PathPointType)pathTypes[i], pathPoints[i].X, pathPoints[i].Y);
			}

			myPath.Flatten(translateMatrix, 10);

			pathPoints = myPath.PathPoints;
			pathTypes = myPath.PathTypes;

			Console.WriteLine("Flatten after Flattening");
			for (int i = 0; i < myPath.PathTypes.Length; i++)
			{
				Console.WriteLine("{0} - {1},{2}", (PathPointType)pathTypes[i], pathPoints[i].X, pathPoints[i].Y);
			}

			g.DrawPath(new Pen(Color.Red, 1), myPath);

			
		}

		public void PathIterator1 (Graphics g)
		{

			// Create a graphics path.
			GraphicsPath myPath = new GraphicsPath ();

			// Set up a points array.
			Point[] myPoints = {
				new Point(20, 20),
				new Point(120, 120),
				new Point(20, 120),
				new Point(20, 20)
			};

			// Create a rectangle.
			Rectangle myRect = new Rectangle(120, 120, 100, 100);

			// Add the points, rectangle, and an ellipse to the path.
			myPath.AddLines(myPoints);
			myPath.SetMarkers();
			myPath.AddRectangle(myRect);
			myPath.SetMarkers();
			myPath.AddEllipse(220, 220, 100, 100);

			// Get the total number of points for the path, and arrays of 
			// the  points and types. 
			int myPathPointCount = myPath.PointCount;
			PointF[] myPathPoints = myPath.PathPoints;
			byte[] myPathTypes = myPath.PathTypes;

			// Set up variables for listing the array of points on the left 
			// side of the screen. 
			int i;
			float j = 20;
			Font myFont = new Font("Arial", 8);
			SolidBrush myBrush = new SolidBrush(Color.Black);

			// List the set of points and types and types to the left side 
			// of the screen. 
			for(i=0; i<myPathPointCount; i++)
			{
				g.DrawString(myPathPoints[i].X.ToString()+
				                      ", " + myPathPoints[i].Y.ToString() + ", " +
				                      myPathTypes[i].ToString(),
				                      myFont,
				                      myBrush,
				                      20,
				                      j);
				j+=20;
			}

			// Create a GraphicsPathIterator for myPath and rewind it.
			GraphicsPathIterator myPathIterator =
				new GraphicsPathIterator(myPath);
			myPathIterator.Rewind();

			// Set up the arrays to receive the copied data.
			PointF[] points = new PointF[myPathIterator.Count];
			byte[] types = new byte[myPathIterator.Count];
			int myStartIndex;
			int myEndIndex;

			// Increment the starting index to the second marker in the 
			// path.
			myPathIterator.NextMarker(out myStartIndex, out myEndIndex);
			myPathIterator.NextMarker(out myStartIndex, out myEndIndex);

			// Copy all the points and types from the starting index to the 
			// ending index to the points array and the types array 
			// respectively. 
			int numPointsCopied = myPathIterator.CopyData(
				ref points,
				ref types,
				myStartIndex,
				myEndIndex);

			// List the copied points to the right side of the screen.
			j = 20;
			int copiedStartIndex = 0;
			for(i=0; i<numPointsCopied; i++)
			{
				copiedStartIndex = myStartIndex + i;
				g.DrawString(
					"Point: " + copiedStartIndex.ToString() +
					", Value: " + points[i].ToString() +
					", Type: " + types[i].ToString(),
					myFont,
					myBrush,
					200,
					j);
				j+=20;
			}

			
		}

		public void PathIterator2 (Graphics g)
		{
			GraphicsPath myPath = new GraphicsPath();
			Point[] myPoints =
			{
				new Point(20, 20),
				new Point(120, 120),
				new Point(20, 120),
				new Point(20, 20)
			};
			Rectangle myRect = new Rectangle(120, 120, 100, 100);
			myPath.AddLines(myPoints);
			myPath.AddRectangle(myRect);
			myPath.AddEllipse(220, 220, 100, 100);

			// Get the total number of points for the path, and arrays of 
			// the  points and types. 
			int myPathPointCount = myPath.PointCount;
			PointF[] myPathPoints = myPath.PathPoints;
			byte[] myPathTypes = myPath.PathTypes;

			// Set up variables for listing the array of points on the left 
			// side of the screen. 
			int i;
			float j = 20;
			Font myFont = new Font("Arial", 8);
			SolidBrush myBrush = new SolidBrush(Color.Black);

			// List the set of points and types and types to the left side 
			// of the screen.
			g.DrawString("Original Data",
			                      myFont,
			                      myBrush,
			                      20,
			                      j);
			j += 20;
			for(i=0; i<myPathPointCount; i++)
			{
				g.DrawString(myPathPoints[i].X.ToString()+
				                      ", " + myPathPoints[i].Y.ToString() + ", " +
				                      myPathTypes[i].ToString(),
				                      myFont,
				                      myBrush,
				                      20,
				                      j);
				j+=20;
			}

			// Create a GraphicsPathIterator for myPath.
			GraphicsPathIterator myPathIterator =
				new GraphicsPathIterator(myPath);
			myPathIterator.Rewind();
			PointF[] points = new PointF[myPathIterator.Count];
			byte[] types = new byte[myPathIterator.Count];
			int numPoints = myPathIterator.Enumerate(ref points, ref types);

			// Draw the set of copied points and types to the screen.
			j = 20;
			g.DrawString("Copied Data",
			                      myFont,
			                      myBrush,
			                      200,
			                      j);
			j += 20;
			for(i=0; i<points.Length; i++)
			{
				g.DrawString("Point: " + i +
				                      ", " + "Value: " + points[i].ToString() + ", " +
				                      "Type: " + types[i].ToString(),
				                      myFont,
				                      myBrush,
				                      200,
				                      j);
				j+=20;
			}

			
		}

		public void PathIterator3(Graphics g)
		{

			// Create the GraphicsPath.
			GraphicsPath myPath = new GraphicsPath();

			Point[] myPoints = {new Point(20, 20), new Point(120, 120), 
				new Point(20, 120),new Point(20, 20) }; 
			Rectangle myRect = new Rectangle(120, 120, 100, 100);

			// Add 3 lines, a rectangle, and an ellipse.
			myPath.AddLines(myPoints);
			myPath.AddRectangle(myRect);
			myPath.AddEllipse(220, 220, 100, 100);

			// List all of the path points to the screen.
			ListPathPoints(g, myPath, null, 20, 1);

			// Create a GraphicsPathIterator.
			GraphicsPathIterator myPathIterator = new
				GraphicsPathIterator(myPath);

			// Rewind the Iterator.
			myPathIterator.Rewind();

			// Iterate the subpaths and types, and list the results to 

			// the screen. 
			int i, j = 20;
			int mySubPaths, subPathStartIndex, subPathEndIndex;
			Boolean IsClosed;
			byte subPathPointType;
			int pointTypeStartIndex,  pointTypeEndIndex, numPointsFound;
			Font myFont = new Font("Arial", 8);
			SolidBrush myBrush = new SolidBrush(Color.Black);
			j = 20;
			for(i = 0;i < 3; i++)
			{
				mySubPaths = myPathIterator.NextSubpath(
					out subPathStartIndex,
					out subPathEndIndex,
					out IsClosed);
				numPointsFound = myPathIterator.NextPathType(
					out subPathPointType,
					out pointTypeStartIndex,
					out pointTypeEndIndex);
				g.DrawString(
					"SubPath: " + i +
					"  Points Found: " + numPointsFound.ToString() +
					"  Type of Points: " + subPathPointType.ToString(),
					myFont,
					myBrush,
					200,
					j);
				j+=20;
			}

			// List the total number of path points to the screen.
			ListPathPoints(g, myPath, myPathIterator, 200, 2);

			
		}

		//------------------------------------------------------- 
		//This function is a helper function used by 
		// NextPathTypeExample. 
		//------------------------------------------------------- 
		public void ListPathPoints(
			Graphics g,
			GraphicsPath myPath,
			GraphicsPathIterator myPathIterator,
			int xOffset,
			int listType)
		{

			// Get the total number of points for the path, 
			// and the arrays of the points and types. 
			int myPathPointCount = myPath.PointCount;
			PointF[] myPathPoints = myPath.PathPoints;
			byte[] myPathTypes = myPath.PathTypes;

			// Set up variables for drawing the points to the screen. 
			int i;
			float j = 20;
			Font myFont = new Font("Arial", 8);
			SolidBrush myBrush = new SolidBrush(Color.Black);
			if (listType == 1) 
				// List all the path points to the screen.
			{

				// Draw the set of path points and types to the screen. 
				for(i=0; i<myPathPointCount; i++)
				{
					g.DrawString(myPathPoints[i].X.ToString()+
					                      ", " + myPathPoints[i].Y.ToString() + ", " +
					                      myPathTypes[i].ToString(),
					                      myFont,
					                      myBrush,
					                      xOffset,
					                      j);
					j+=20;
				}
			}
			else if (listType == 2) 
				// Display the total number of path points.
			{

				// Draw the total number of points to the screen. 
				int myPathTotalPoints = myPathIterator.Count;
				g.DrawString("Total Points = " +
				                      myPathTotalPoints.ToString(),
				                      myFont,
				                      myBrush,
				                      xOffset,
				                      100);
			}
			else
			{
				g.DrawString("Wrong or no list type argument.",
				                      myFont, myBrush, xOffset, 200);
			}
		}

		public void PathIterator4(Graphics g)
		{

			// Create a graphics path.
			GraphicsPath myPath = new GraphicsPath();

			// Set up primitives to add to myPath.
			Point[] myPoints = {new Point(20, 20), new Point(120, 120), 
				new Point(20, 120),new Point(20, 20) }; 
			Rectangle myRect = new Rectangle(120, 120, 100, 100);

			// Add 3 lines, a rectangle, an ellipse, and 2 markers.
			myPath.AddLines(myPoints);
			myPath.SetMarkers();
			myPath.AddRectangle(myRect);
			myPath.SetMarkers();
			myPath.AddEllipse(220, 220, 100, 100);

			// Get the total number of points for the path, 

			// and the arrays of the points and types. 
			int myPathPointCount = myPath.PointCount;
			PointF[] myPathPoints = myPath.PathPoints;
			byte[] myPathTypes = myPath.PathTypes;

			// Set up variables for listing all of the path's 

			// points to the screen. 
			int i;
			float j = 20;
			Font myFont = new Font("Arial", 8);
			SolidBrush myBrush = new SolidBrush(Color.Black);

			// List the values of all the path points and types to the screen. 
			for(i=0; i<myPathPointCount; i++)
			{
				g.DrawString(myPathPoints[i].X.ToString()+
				                      ", " + myPathPoints[i].Y.ToString() + ", " +
				                      myPathTypes[i].ToString(),
				                      myFont,
				                      myBrush,
				                      20,
				                      j);
				j+=20;
			}

			// Create a GraphicsPathIterator for myPath.
			GraphicsPathIterator myPathIterator = new
				GraphicsPathIterator(myPath);

			// Rewind the iterator.
			myPathIterator.Rewind();

			// Create the GraphicsPath section.
			GraphicsPath myPathSection = new GraphicsPath();

			// Iterate to the 3rd subpath and list the number of points therein 

			// to the screen. 
			int subpathPoints;
			bool IsClosed2;

			// Iterate to the third subpath.
			subpathPoints = myPathIterator.NextSubpath(
				myPathSection, out IsClosed2);
			subpathPoints = myPathIterator.NextSubpath(
				myPathSection, out IsClosed2);
			subpathPoints = myPathIterator.NextSubpath(
				myPathSection, out IsClosed2);

			// Write the number of subpath points to the screen.
			g.DrawString("Subpath: 3"  +
			                      "   Num Points: " +
			                      subpathPoints.ToString(),
			                      myFont,
			                      myBrush,
			                      200,
			                      20);

			
		}

		public void PathIterator5(Graphics g)
		{

			// Create a graphics path.
			GraphicsPath myPath = new GraphicsPath();

			// Set up primitives to add to myPath.
			Point[] myPoints = {new Point(20, 20), new Point(120, 120), 
				new Point(20, 120),new Point(20, 20) };        

			Rectangle myRect = new Rectangle(120, 120, 100, 100);

			// Add 3 lines, a rectangle, an ellipse, and 2 markers.
			myPath.AddLines(myPoints);
			myPath.SetMarkers();
			myPath.AddRectangle(myRect);
			myPath.SetMarkers();
			myPath.AddEllipse(220, 220, 100, 100);

			// Get the total number of points for the path, 
			// and the arrays of the points and types. 
			int myPathPointCount = myPath.PointCount;
			PointF[] myPathPoints = myPath.PathPoints;
			byte[] myPathTypes = myPath.PathTypes;

			// Set up variables for listing all the values of the path's 
			// points to the screen. 
			int i;
			float j = 20;
			Font myFont = new Font("Arial", 8);
			SolidBrush myBrush = new SolidBrush(Color.Black);

			// List the values for all of path points and types to 
			// the left side of the screen. 
			for(i=0; i < myPathPointCount; i++)
			{
				g.DrawString(myPathPoints[i].X.ToString()+
				                      ", " + myPathPoints[i].Y.ToString() + ", " +
				                      myPathTypes[i].ToString(),  myFont, myBrush,
				                      20, j);

				j+=20; 

			}

			// Create a GraphicsPathIterator.
			GraphicsPathIterator myPathIterator = new
				GraphicsPathIterator(myPath);

			// Rewind the iterator.
			myPathIterator.Rewind();

			// Create a GraphicsPath to receive a section of myPath.
			GraphicsPath myPathSection = new GraphicsPath();

			// Retrieve and list the number of points contained in 

			// the first marker to the right side of the screen. 
			int markerPoints;
			markerPoints = myPathIterator.NextMarker(myPathSection);
			g.DrawString("Marker: 1" + "  Num Points: " +
			                      markerPoints.ToString(),  myFont, myBrush, 200, 20);

			

		}

		private void PathIterator6(Graphics g)
		{

			// Create the GraphicsPath.
			GraphicsPath myPath = new GraphicsPath();
			Point[] myPoints = {new Point(20, 20), new Point(120, 120), 
				new Point(20, 120),new Point(20, 20) }; 

			Rectangle myRect = new Rectangle(120, 120, 100, 100);

			// Add 3 lines, a rectangle, an ellipse, and 2 markers.
			myPath.AddLines(myPoints);
			myPath.SetMarkers();
			myPath.AddRectangle(myRect);
			myPath.SetMarkers();
			myPath.AddEllipse(220, 220, 100, 100);

			// Get the total number of points for the path, 

			// and the arrays of the points and types. 
			int myPathPointCount = myPath.PointCount;
			PointF[] myPathPoints = myPath.PathPoints;
			byte[] myPathTypes = myPath.PathTypes;

			// Set up variables for drawing the array 

			// of points to the screen. 
			int i;
			float j = 20;
			Font myFont = new Font("Arial", 8);
			SolidBrush myBrush = new SolidBrush(Color.Black);

			// Draw the set of path points and types to the screen. 
			for(i=0; i<myPathPointCount; i++)
			{
				g.DrawString(myPathPoints[i].X.ToString()+
				                      ", " + myPathPoints[i].Y.ToString() + ", " +
				                      myPathTypes[i].ToString(),
				                      myFont,
				                      myBrush,
				                      20,
				                      j);
				j+=20;
			}

			// Create a GraphicsPathIterator.
			GraphicsPathIterator myPathIterator = new
				GraphicsPathIterator(myPath);
			int myStartIndex;
			int myEndIndex;

			// Rewind the Iterator.
			myPathIterator.Rewind();

			// Draw the Markers and their start and end points 

			// to the screen.
			j=20;
			for(i=0;i<3;i++)
			{
				myPathIterator.NextMarker(out myStartIndex, out myEndIndex);
				g.DrawString("Marker " + i.ToString() +
				                      ":  Start: " + myStartIndex.ToString()+
				                      "  End: " + myEndIndex.ToString(),
				                      myFont,
				                      myBrush,
				                      200,
				                      j);
				j += 20;
			}

			// Draw the total number of points to the screen.
			j += 20;
			int myPathTotalPoints = myPathIterator.Count;
			g.DrawString("Total Points = " +
			                      myPathTotalPoints.ToString(),
			                      myFont,
			                      myBrush,
			                      200,
			                      j);

			
		}

		public void PathIterator7(Graphics g)
		{

			// Create a graphics path.
			GraphicsPath myPath = new GraphicsPath();

			// Set up primitives to add to myPath.
			Point[] myPoints = {new Point(20, 20), new Point(120, 120), 
				new Point(20, 120),new Point(20, 20) }; 
			Rectangle myRect = new Rectangle(120, 120, 100, 100);

			// Add 3 lines, a rectangle, an ellipse, and 2 markers.
			myPath.AddLines(myPoints);
			myPath.SetMarkers();
			myPath.AddRectangle(myRect);
			myPath.SetMarkers();
			myPath.AddEllipse(220, 220, 100, 100);

			// Get the total number of points for the path, 

			// and the arrays of the points and types. 
			int myPathPointCount = myPath.PointCount;
			PointF[] myPathPoints = myPath.PathPoints;
			byte[] myPathTypes = myPath.PathTypes;

			// Set up variables for listing all of the path's 

			// points to the screen. 
			int i;
			float j = 20;
			Font myFont = new Font("Arial", 8);
			SolidBrush myBrush = new SolidBrush(Color.Black);

			// List the values of all the path points and types to the screen. 
			for(i=0; i<myPathPointCount; i++)
			{
				g.DrawString(myPathPoints[i].X.ToString()+
				                      ", " + myPathPoints[i].Y.ToString() + ", " +
				                      myPathTypes[i].ToString(),
				                      myFont,
				                      myBrush,
				                      20,
				                      j);
				j+=20;
			}

			// Create a GraphicsPathIterator for myPath.
			GraphicsPathIterator myPathIterator = new
				GraphicsPathIterator(myPath);

			// Rewind the iterator.
			myPathIterator.Rewind();

			// Create the GraphicsPath section.
			GraphicsPath myPathSection = new GraphicsPath();

			// Iterate to the 3rd subpath and list the number of points therein 

			// to the screen. 
			int subpathPoints;
			bool IsClosed2;

			// Iterate to the third subpath.
			subpathPoints = myPathIterator.NextSubpath(
				myPathSection, out IsClosed2);
			subpathPoints = myPathIterator.NextSubpath(
				myPathSection, out IsClosed2);
			subpathPoints = myPathIterator.NextSubpath(
				myPathSection, out IsClosed2);

			// Write the number of subpath points to the screen.
			g.DrawString("Subpath: 3"  +
			                      "   Num Points: " +
			                      subpathPoints.ToString(),
			                      myFont,
			                      myBrush,
			                      200,
			                      20);

			
		}

		private void Widen1(Graphics g)
		{

			// Create a path and add two ellipses.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddEllipse(50, 50, 100, 100);
			myPath.AddEllipse(150, 50, 100, 100);

			// Draw the original ellipses to the screen in black.
			g.DrawPath(Pens.Black, myPath);

			// Widen the path.
			Pen widenPen = new Pen(Color.Black, 10);
			Matrix widenMatrix = new Matrix();
			widenMatrix.Translate(50, 50);
			myPath.Widen(widenPen, widenMatrix, 1.0f);

			// Draw the widened path to the screen in red.
			g.FillPath(new SolidBrush(Color.Red), myPath);		
		
			//g.DrawPath (Pens.Black, myPath);

			

		}

		private void Widen2(Graphics g)
		{

			// Create a path and add two ellipses.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddLines(new Point[] { new Point(20, 10),
				new Point(50, 50),
				new Point(80, 10) });

			myPath.AddPolygon(new Point[] { new Point(20, 30),
				new Point(50, 70),
				new Point(80, 30) });

			var cx = Bounds.Width;
			var cy = Bounds.Height;

			var clr = Color.Aquamarine;

			g.ScaleTransform((float)(cx / 300f), (float)(cy / 200f));

			for (int i = 0; i < 6; i++)
			{
				GraphicsPath pathClone = (GraphicsPath)myPath.Clone();
				Matrix matrix = new Matrix();
				Pen penThin = new Pen(clr, 1);
				Pen penThick = new Pen(clr, 5);
				Pen penWiden = new Pen(clr, 7.5f);
				Brush brush = new SolidBrush(clr);

				matrix.Translate((i % 3) * 100, (i / 3) * 100);

				if (i < 3)
					pathClone.Transform(matrix);
				else
					pathClone.Widen(penWiden, matrix);

				switch (i % 3)
				{
					case 0: g.DrawPath(penThin, pathClone); break;
					case 1: g.DrawPath(penThick, pathClone); break;
					case 2: g.FillPath(brush, pathClone); break;
				}
			}
			

		}

		// Example code taken from here : http://www.codeproject.com/Articles/38436/Extended-Graphics-Rounded-rectangles-Font-metrics
		// Wanted to test the graphics path and LinearGradientBrush code together.
		void RoundeRectangle1(Graphics g)
		{
			var width = this.ClientRectangle.Width;
			var height = this.ClientRectangle.Height;

			var GradientInactiveCaption = Color.FromArgb (255, 215, 228, 242);
			var GradientActiveCaptionDark2 = Color.FromArgb (255, 52, 112, 171);
			var GradientActiveCaptionDark5 = Color.FromArgb (255, 33, 79, 107);
			var InactiveBorderLight = Color.FromArgb (255, 143, 247, 253);

			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.FillRoundedRectangle(new SolidBrush(GradientActiveCaptionDark2), 10, 10, width - 40, height - 60, 10);
			LinearGradientBrush brush = new LinearGradientBrush(
				new Point(width/2, 0),
				new Point(width/2, height),
				GradientInactiveCaption,
				GradientActiveCaptionDark5
				);
			g.FillRoundedRectangle(brush, 12, 12, width - 44, height - 64, 10);
			g.DrawRoundedRectangle(new Pen(InactiveBorderLight), 12, 12, width - 44, height - 64, 10);
			g.FillRoundedRectangle(new SolidBrush(Color.FromArgb(100, 70, 130, 180)), 12, 12 + ((height - 64) / 2), width - 44, (height - 64)/2, 10);

			
		}

		
		private void AddString1(Graphics g)
		{

			// Create a GraphicsPath object.
			GraphicsPath myPath = new GraphicsPath();

			// Set up all the string parameters. 
			//string stringText = "Sample Text";
			string stringText = "Sample Text";
			FontFamily family = new FontFamily("Arial");
			int fontStyle = (int)FontStyle.Italic;
			int emSize = 26;
			Point origin = new Point(50, 50);
			StringFormat format = StringFormat.GenericDefault;

			var size = g.MeasureString (stringText, new Font (family, emSize));
			//format.Alignment = StringAlignment.Far;
			format.LineAlignment = StringAlignment.Far;
			myPath.AddRectangle (new RectangleF (origin.X, origin.Y, size.Width, size.Height));
			// Add the string to the path.
			myPath.AddString(stringText,
			                 family,
			                 fontStyle,
			                 emSize,
			                 origin,
			                 format);

			//Draw the path to the screen.
			//g.FillPath(Brushes.Black, myPath);
			g.DrawPath (Pens.Blue, myPath);

			//OutputPaths("", myPath);

			

		}

		private void AddString2(Graphics g)
		{

			// Create a GraphicsPath object.
			GraphicsPath myPath = new GraphicsPath();
			g.SmoothingMode = SmoothingMode.HighQuality;
			// Set up all the string parameters. 
			//string stringText = "Sample Text";
			string stringText = "Now is the time for all good men to come to the aid of their country";
			FontFamily family = new FontFamily("Arial");
			int fontStyle = (int)FontStyle.Italic;
			int emSize = 26;
			PointF origin = new PointF(20, 20);
			SizeF sizeLayout = new SizeF (ClientRectangle.Size.Width - origin.X * 2, ClientRectangle.Size.Height - origin.Y * 2);
			StringFormat format = StringFormat.GenericDefault;

			var size = g.MeasureString (stringText, new Font (family, emSize));
			format.Alignment = StringAlignment.Far;
			format.LineAlignment = StringAlignment.Center;
			myPath.AddRectangle (new RectangleF (origin.X, origin.Y, sizeLayout.Width, sizeLayout.Height));
			// Add the string to the path.
			myPath.AddString(stringText,
			                 family,
			                 fontStyle,
			                 emSize,
			                 new RectangleF(origin,sizeLayout),
			                 format);

			//Draw the path to the screen.
			//g.FillPath(Brushes.Black, myPath);
			g.DrawPath (Pens.Blue, myPath);

			//OutputPaths("", myPath);

			

		}

		private void AddString3(Graphics g)
		{

			//create a path
			GraphicsPath pth = new GraphicsPath();
			//Add a string               
			pth.AddString("Outline Text.",
			              new FontFamily("Times New Roman"),0,50,
			              new Point(30,30), StringFormat.GenericTypographic);
			//Select the pen             
			Pen p=new Pen(Color.Blue,1.0f);
			//draw the hollow outlined text
			g.DrawPath(p,pth);
			//clear the path
			pth.Reset();
			//Add new text
			pth.AddString("Filled outline Text.",
			              new FontFamily("Papyrus"),0,35,
			              new Point(30,120),StringFormat.GenericTypographic);
			//Fill it
			g.FillPath(Brushes.Red,pth);
			//outline it
			g.DrawPath(p,pth);
			//tidy up.
			p.Dispose();
			pth.Dispose();


			

		}
		
		private void AddString4(Graphics g)
		{
			g.Clear (Color.FromArgb(255,204,204,204));

			var myPath = new GraphicsPath ();

			FontFamily fntFamily = new FontFamily("Times New Roman");
			string s="Embossed Text";

			myPath.AddString (s, fntFamily, 0, 50, new PointF (28, 28), StringFormat.GenericTypographic);
			g.FillPath (Brushes.White, myPath);

			myPath.Reset ();
			myPath.AddString (s, fntFamily, 0, 50, new PointF (32, 32), StringFormat.GenericTypographic);
			g.FillPath (Brushes.DarkGray, myPath);

			myPath.Reset ();
			myPath.AddString (s, fntFamily, 0, 50, new PointF (30, 30), StringFormat.GenericTypographic);
			g.FillPath (Brushes.Black, myPath);

			s="Chiseled Text";
			myPath.Reset ();
			myPath.AddString (s, fntFamily, 0, 50, new PointF (28, 108), StringFormat.GenericTypographic);
			g.FillPath (Brushes.DarkGray, myPath);

			myPath.Reset ();
			myPath.AddString (s, fntFamily, 0, 50, new PointF (32, 112), StringFormat.GenericTypographic);
			g.FillPath (Brushes.LightGray, myPath);

			myPath.Reset ();
			myPath.AddString (s, new FontFamily ("Times New Roman"), 0, 50, new PointF (30, 110), StringFormat.GenericTypographic);
			g.FillPath (Brushes.SeaShell, myPath);

			fntFamily.Dispose();

			
		}


		private void IsVisible1(Graphics g)
		{

			// Create a path and add an ellipse.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddEllipse(0, 0, 100, 100);

			g.DrawPath(Pens.Blue, myPath);

			// Test the visibility of point (50, 50). 
			bool visible = myPath.IsVisible(50, 50, g);
			g.FillRectangle(Brushes.Green, new Rectangle(48, 48, 2, 2));
			// Show the result.
			g.DrawString("Visible = " + visible, new Font("Arial", 12), Brushes.Green, 60, 50);

			visible = myPath.IsVisible(90, 90, g);
			g.FillRectangle(Brushes.Red, new Rectangle(88, 88, 2, 2));
			// Show the result.
			g.DrawString("Visible = " + visible, new Font("Arial", 12), Brushes.Red, 100, 90);

			
		}

		
		private void IsVisible2(Graphics g)
		{

			// Create a path and add an ellipse.
			GraphicsPath myPath = new GraphicsPath();
			myPath.AddEllipse(0, 0, 100, 100);

			g.DrawPath(Pens.Blue, myPath);

			// Test the visibility of point (50, 50). 
			bool visible = myPath.IsVisible(new PointF(50, 50), g);
			g.FillRectangle(Brushes.Green, new Rectangle(48, 48, 2, 2));
			// Show the result.
			g.DrawString("Visible = " + visible, new Font("Arial", 12), Brushes.Green, 60, 50);

			visible = myPath.IsVisible(new PointF(90, 90), g);
			g.FillRectangle(Brushes.Red, new Rectangle(88, 88, 2, 2));
			// Show the result.
			g.DrawString("Visible = " + visible, new Font("Arial", 12), Brushes.Red, 100, 90);

			
		}


		private void SetClip1 (Graphics g)
		{

			// Create graphics path.
			GraphicsPath clipPath = new GraphicsPath();
			clipPath.AddEllipse(0, 0, 200, 100);
			Matrix transform = new Matrix ();
			transform.Translate (100, 100);
			clipPath.Transform (transform);
			// Set clipping region to path.
			g.SetClip(clipPath);

			// Fill rectangle to demonstrate clipping region.
			g.FillRectangle(new SolidBrush(Color.Blue), 0, 0, 500, 300);

			
		}

		private void SetClip2(Graphics g)
		{

			g.SetClip (new RectangleF (50, 50, 100, 100));

			// Create graphics path.
			GraphicsPath clipPath = new GraphicsPath();
			clipPath.AddEllipse(75, 75, 200, 100);

			// Set clipping region to path.
			g.SetClip(clipPath, CombineMode.Intersect);

			// Fill rectangle to demonstrate clipping region.
			g.FillRectangle(new SolidBrush(Color.Blue), 0, 0, 500, 300);

			
		}

		
		private void SetClip3(Graphics g)
		{

			g.SetClip (new RectangleF (50, 50, 100, 100));

			// Create graphics path.
			GraphicsPath clipPath = new GraphicsPath();
			clipPath.AddEllipse(75, 75, 200, 100);

			// Set clipping region to path.
			g.SetClip(clipPath, CombineMode.Union);

			// Fill rectangle to demonstrate clipping region.
			g.FillRectangle(new SolidBrush(Color.Blue), 0, 0, 500, 300);

			
		}

		
		private void SetClip4(Graphics g)
		{

			g.SetClip (new RectangleF (50, 50, 100, 100));

			// Create graphics path.
			GraphicsPath clipPath = new GraphicsPath();
			clipPath.AddEllipse(75, 75, 200, 100);

			// Set clipping region to path.
			g.SetClip(clipPath, CombineMode.Exclude);

			// Fill rectangle to demonstrate clipping region.
			g.FillRectangle(new SolidBrush(Color.Blue), 0, 0, 500, 300);

			
		}

		
		private void SetClip5(Graphics g)
		{

			g.SetClip (new RectangleF (50, 50, 100, 100));

			// Create graphics path.
			GraphicsPath clipPath = new GraphicsPath();
			clipPath.AddEllipse(75, 75, 200, 100);

			// Set clipping region to path.
			g.SetClip(clipPath, CombineMode.Xor);

			// Fill rectangle to demonstrate clipping region.
			g.FillRectangle(new SolidBrush(Color.Blue), 0, 0, 500, 300);

			
		}
		
		public void IsOutlineVisible1(Graphics g)
		{
			GraphicsPath myPath = new GraphicsPath();
			Rectangle rect = new Rectangle(20, 20, 100, 100);
			myPath.AddRectangle(rect);
			Pen testPen = new Pen(Color.AliceBlue, 20);
			var widePath = (GraphicsPath)myPath.Clone();
			widePath.Widen(testPen);
			g.FillPath(Brushes.Wheat, widePath);
			g.DrawPath(Pens.Black, myPath);

			var point = new PointF(100, 50);

			bool visible = myPath.IsOutlineVisible(point, testPen, g);
			g.FillRectangle(Brushes.Red, new RectangleF(point.X, point.Y, 2, 2));
			// Show the result.
			g.DrawString("Visible = " + visible, new Font("Arial", 12), Brushes.Red, point.X + 10, point.Y);

			point.X = 115;
			point.Y = 80;

			visible = myPath.IsOutlineVisible(point, testPen, g);
			g.FillRectangle(Brushes.Green, new RectangleF(point.X, point.Y, 2, 2));
			// Show the result.
			g.DrawString("Visible = " + visible, new Font("Arial", 12), Brushes.Green, point.X + 10, point.Y);

			
		}

		private void Warp1 (Graphics g)
		{
			//create a path
			GraphicsPath pth = new GraphicsPath();
			string s = "Star Wars Warped!!";
			FontFamily ff = new FontFamily("Verdana");
			//Add the text strings
			for (int y = 0; y < 5; y++)
			{
				pth.AddString(s, ff, 0, 70, new Point(0, 90 * y), StringFormat.GenericTypographic);
			}

			//Create the warp array
			PointF[] points = new PointF[]{
				new PointF(this.ClientRectangle.Width/2-this.ClientRectangle.Width/4,0),
				new PointF(this.ClientRectangle.Width/2+this.ClientRectangle.Width/4,0),
				new PointF(0,this.ClientRectangle.Height),
				new PointF(this.ClientRectangle.Width,this.ClientRectangle.Height)
			};
			var rect = new RectangleF(0, 0, 1000, 500);

			//Warp the path
			pth.Warp(points, rect);

			//Fill the background
			g.FillRectangle(Brushes.DarkGray, this.ClientRectangle);
			//Paint the warped path by filling it
			g.FillPath(Brushes.Yellow, pth);
			pth.Dispose();

			

		}

		
		private void Warp2(Graphics g)
		{

			// Create a path and add a rectangle.
			GraphicsPath myPath = new GraphicsPath();
			RectangleF srcRect = new RectangleF(0, 0, 100, 200);
			myPath.AddRectangle(srcRect);

			// Draw the source path (rectangle)to the screen.
			g.DrawPath(Pens.Black, myPath);

			// Create a destination for the warped rectangle.
			PointF point1 = new PointF(200, 200);
			PointF point2 = new PointF(400, 250);
			PointF point3 = new PointF(220, 400);
			PointF[] destPoints = { point1, point2, point3 };

			// Create a translation matrix.
			Matrix translateMatrix = new Matrix();
			translateMatrix.Translate(0, 0);

			// Warp the source path (rectangle).
			myPath.Warp(destPoints, srcRect, translateMatrix,
			            WarpMode.Perspective, 0.5f);

			// Draw the warped path (rectangle) to the screen.
			g.DrawPath(new Pen(Color.Red), myPath);

			
		} 


		private void Warp3(Graphics g)
		{

			// Create a path and add a rectangle.
			GraphicsPath myPath = new GraphicsPath();
			string s = "Warp Perspective";
			FontFamily ff = new FontFamily("Chalkduster");
			var emSize = 30;
			var font = new Font (ff, emSize);
			var sSize = g.MeasureString (s, font);
			var location = new PointF (50,100);
			var srcRect = new RectangleF (location, sSize);
			g.DrawRectangle (Pens.Blue, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height);
			//Add the text string
			myPath.AddString(s, ff, 0, emSize, srcRect.Location, StringFormat.GenericTypographic);


			// Create a destination for the warped rectangle.
			PointF point1 = location;
			point1.X -= 30;
			point1.Y -= 40;
			PointF point2 = new PointF(srcRect.Right, srcRect.Top);
			point2.Y += 10;
			PointF point3 = new PointF (srcRect.Left, srcRect.Bottom);
			PointF point4 = new PointF (srcRect.Right, srcRect.Bottom);

			PointF[] destPoints = { point1, point2, point3, point4 };

			// draw the destination points to the screen
			g.DrawLines (Pens.Green, new PointF[] {point1, point2, point4, point3, point1});

			// Create a translation matrix.
			Matrix translateMatrix = new Matrix();
			translateMatrix.Translate(0, 0);

			// Warp the source path (rectangle).
			myPath.Warp(destPoints, srcRect, translateMatrix,
			            WarpMode.Perspective);

			// Draw the warped path (rectangle) to the screen.
			g.FillPath(new HatchBrush(HatchStyle.HorizontalBrick, Color.Red), myPath);

			
		} 


		
		private void Warp4(Graphics g)
		{

			// Create a path and add a rectangle.
			GraphicsPath myPath = new GraphicsPath();
			string s = "Warp Bilinear";
			FontFamily ff = new FontFamily("Chalkduster");
			var emSize = 30;
			var font = new Font (ff, emSize);
			var sSize = g.MeasureString (s, font);
			var location = new PointF (50,100);
			var srcRect = new RectangleF (location, sSize);
			g.DrawRectangle (Pens.Blue, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height);
			//Add the text string
			myPath.AddString(s, ff, 0, emSize, srcRect.Location, StringFormat.GenericTypographic);


			// Create a destination for the warped rectangle.
			PointF point1 = location;
			point1.X -= 30;
			point1.Y -= 40;
			PointF point2 = new PointF(srcRect.Right, srcRect.Top);
			point2.Y += 10;
			PointF point3 = new PointF (srcRect.Left, srcRect.Bottom);
			PointF point4 = new PointF (srcRect.Right, srcRect.Bottom);

			PointF[] destPoints = { point1, point2, point3, point4 };

			// draw the destination points to the screen
			g.DrawLines (Pens.Green, new PointF[] {point1, point2, point4, point3, point1});

			// Create a translation matrix.
			Matrix translateMatrix = new Matrix();
			translateMatrix.Translate(0, 0);

			// Warp the source path (rectangle).
			myPath.Warp(destPoints, srcRect, translateMatrix,
			            WarpMode.Perspective);

			// Draw the warped path (rectangle) to the screen.
			g.FillPath(new HatchBrush(HatchStyle.HorizontalBrick, Color.Red), myPath);

			
		} 


		public void OutputPaths (string title, GraphicsPath myPath)
		{

			var iterator = new GraphicsPathIterator(myPath);
			int startIndex, endIndex;
			bool isClosed;
			int subPaths = iterator.SubpathCount;
			var pathData = myPath.PathData;

			Console.WriteLine("{0} - num paths {1}", title, subPaths);
			for (int sp = 0; sp < subPaths; sp++)
			{

				var numOfPoints = iterator.NextSubpath(out startIndex, out endIndex, out isClosed);
				Console.WriteLine("subPath {0} - from {1} to {2} closed {3}", sp + 1, startIndex, endIndex, isClosed);

				for (int pp = startIndex; pp <= endIndex; pp++)
				{
					Console.WriteLine("         {0} - {1}", pathData.Points[pp], (PathPointType)pathData.Types[pp]);
				}

			}


		}
	}
}

