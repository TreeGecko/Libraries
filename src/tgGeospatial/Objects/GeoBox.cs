using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Common.Interfaces;
using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Geospatial.Helpers;
using TreeGecko.Library.Geospatial.Extensions;

namespace TreeGecko.Library.Geospatial.Objects
{
    public class GeoBox : GeoPolygon, ITGSerializable
    {
        public GeoPoint Center { get; private set; }
		
        public GeoPoint TopLeft { get; private set; }
        public GeoPoint TopRight { get; private set; }
        public GeoPoint BottomRight { get; private set; }
        public GeoPoint BottomLeft { get; private set; }
		
		public GeoPoint East { get; private set; }
		public GeoPoint West { get; private set; }
		public GeoPoint North { get; private set; }
		public GeoPoint South { get; private set; }
		
        public GeoBox()
        {
        }

        public GeoBox(double _east,
                      double _west,
                      double _north,
                      double _south)
        {
            GeoPoint topLeft = new GeoPoint(_west, _north);
            GeoPoint bottomRight = new GeoPoint(_east, _south);

            Setup(topLeft, bottomRight);
        }

        private void Setup(GeoPoint _topLeft, GeoPoint _bottomRight)
        {
            if (_topLeft != null && _bottomRight != null)
            {
                GeoPoint topRight = new GeoPoint(_bottomRight.X, _topLeft.Y);
                GeoPoint bottomLeft = new GeoPoint(_topLeft.X, _bottomRight.Y);

                TopLeft = _topLeft;
                TopRight = topRight;
                BottomLeft = bottomLeft;
                BottomRight = _bottomRight;

                SetPoints();
                SetCenter();

                CalculatePoints();
            }
        }

        public GeoBox(GeoPoint _topLeft, GeoPoint _bottomRight)
        {
            Setup(_topLeft, _bottomRight);
        }

        private void SetPoints()
        {
            Points.Add(TopLeft);
            Points.Add(TopRight);
            Points.Add(BottomRight);
            Points.Add(BottomLeft);
        }
		
		/// <summary>
		/// Sets the center.
		/// </summary>
        private void SetCenter()
        {
            Center = new GeoPoint((TopRight.X + TopLeft.X) / 2, (TopRight.Y + BottomRight.Y) / 2);
        }
		
		/// <summary>
		/// Initializes a new instance of the <see cref="GeoBox"/> class.
		/// </summary>
		/// <param name='_center'>
		/// Center.
		/// </param>
		/// <param name='_width'>
		/// Width.
		/// </param>
		/// <param name='_height'>
		/// Height.
		/// </param>
        public GeoBox(GeoPoint _center, GeoDistance _width, GeoDistance _height)
        {
            Center = _center;

			East  = PositionHelper.GetPointToEast(_center, _width);
			West = PositionHelper.GetPointToWest(_center, _width);
			North = PositionHelper.GetPointToNorth(_center, _height);
			South = PositionHelper.GetPointToSouth(_center, _height);
			
			CalculateCorners();
        }
		
		/// <summary>
		/// Calculates the points.
		/// </summary>
		private void CalculateCorners()
		{
			if (East != null 
			    && West != null
			    && North != null
			    && South != null)
			{
				TopLeft = new GeoPoint(West.X, North.Y);
				TopRight = new GeoPoint(East.X, North.Y);
				BottomLeft = new GeoPoint(West.X, South.Y);
				BottomRight = new GeoPoint(East.X, South.Y);

                SetPoints();
			}
		}
		
		private void CalculatePoints()
		{
			if (TopLeft != null 
			    && TopRight != null
			    && BottomLeft != null
			    && BottomRight != null)
			{
				East = new GeoPoint(TopRight.X, (TopRight.Y + BottomRight.Y)/2 );
				West = new GeoPoint(TopLeft.X, (TopLeft.Y + BottomLeft.Y)/2 );
				North = new GeoPoint((TopLeft.X + TopRight.X)/2, TopLeft.Y);
				South = new GeoPoint((BottomLeft.X + BottomRight.X) / 2, BottomLeft.Y);
			}
		}		
		
        public TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject obj = new TGSerializedObject
                                          {
                                              TGObjectType = ReflectionHelper.GetTypeName(GetType())
                                          };

            obj.Add("TopLeft", TopLeft);
            obj.Add("TopRight", TopRight);
            obj.Add("BottomRight", BottomRight);
            obj.Add("BottomLeft", BottomLeft);
			
			CalculatePoints();

            return obj;
        }

        public void LoadFromTGSerializedObject(TGSerializedObject _tg)
        {
            TopLeft = _tg.GetITGSerializableObject<GeoPoint>("TopLeft");
            TopRight = _tg.GetITGSerializableObject<GeoPoint>("TopRight");
            BottomRight = _tg.GetITGSerializableObject<GeoPoint>("BottomRight");
            BottomLeft = _tg.GetITGSerializableObject<GeoPoint>("BottomLeft");

            SetPoints();
            SetCenter();
        }
		

        public string TGObjectType
        {
            get
            {
                return ReflectionHelper.GetTypeName(GetType());
            }
        }
		
		/// <summary>
		/// Deltas the latitude.
		/// </summary>
		/// <returns>
		/// The latitude.
		/// </returns>
		public double DeltaLatitude()
		{
			if (TopLeft != null && BottomLeft != null)
			{
				return TopLeft.Y - BottomLeft.Y;
			}
			
			return 0;
		}
		
		/// <summary>
		/// Deltas the longitude.
		/// </summary>
		/// <returns>
		/// The longitude.
		/// </returns>
		public double DeltaLongitude()
		{
			if (TopRight != null && TopLeft != null)
			{
				return TopRight.X - TopLeft.X;
			}
			
			return 0;
		}
		
		/// <summary>
		/// Minimums the latitude.
		/// </summary>
		/// <returns>
		/// The latitude.
		/// </returns>
		public double MinLatitude()
		{
			if (BottomLeft != null)
			{
				return BottomLeft.Y;
			}
			
			return 0;
		}
		
		/// <summary>
		/// Maxs the latitude.
		/// </summary>
		/// <returns>
		/// The latitude.
		/// </returns>
		public double MaxLatitude()
		{
			if (TopRight != null)
			{
				return TopRight.Y;
			}
			
			return 0;
		}
		
		/// <summary>
		/// Minimums the longitude.
		/// </summary>
		/// <returns>
		/// The longitude.
		/// </returns>
		public double MinLongitude()
		{
			if (TopLeft != null)
			{
				return TopLeft.X;
			}
			
			return 0;
		}
		
		/// <summary>
		/// Maxs the longitude.
		/// </summary>
		/// <returns>
		/// The longitude.
		/// </returns>
		public double MaxLongitude()
		{
			if (TopRight != null)
			{
				return TopRight.X;
			}
			
			return 0;
		}



        public bool Contains(GeoPoint _point)
        {
            if (_point != null)
            {
                if (East != null && West != null && South != null && North != null)
                {
                    if (_point.X > West.X
                        && _point.X < East.X
                        && _point.Y > South.Y
                        && _point.Y < North.Y)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
