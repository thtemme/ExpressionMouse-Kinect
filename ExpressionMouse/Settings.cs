using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace ExpressionMouse
{
    [XmlRoot("Settings"), XmlType("Settings")]
    public class Settings
    {
        public decimal ClickDelay{get; set;}
        public string HeadrotationSmoothingFilterValues { get; set; }
        public decimal PercentageHorizontalEdgePixels { get; set; }
        public decimal UsedFramesForClosedEyeDetection { get; set; }
        public decimal EyeClosedFilterThreshold { get; set; }
        public decimal DoubleClickSecondEyeThreshold { get; set; }
        public decimal BrowRaiserStartThreshold { get; set; }
        public decimal BrowLowererStartthreshold { get; set; }
        public decimal MouthOpenStartThreshold { get; set; }
        public decimal MouthOpenConfirmation { get; set;}
        public decimal MouthOpenEndThreshold { get; set; }
        public decimal ScrollMultiplierUp { get; set; }
        public decimal ScrollMultiplierDown { get; set; }
        public decimal HeadToScreenRelationX { get; set; }
        public decimal HeadToScreenRelationY { get; set; }
    }
}
