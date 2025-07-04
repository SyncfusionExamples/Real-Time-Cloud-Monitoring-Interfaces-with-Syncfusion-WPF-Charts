﻿using System.Windows.Media;

namespace CloudMonitoring
{
    public class CloudMetricsDataModel
    {
        public string? Name { get; set; }
        public double Value { get; set; }
    }

    public class CloudPerformanceDataModel
    {
        public string? Name { get; set; }
        public double Value { get; set; }
        public DateTime Time{ get; set; }
        public double CPUUsage{ get; set; }
        public double NetworkIn {  get; set; }
        public double NetworkOut { get; set; }
        public double Read { get; set; }
        public double Write { get; set; }
    }

    public class InstanceInfo
    {
        public string InstanceID {  get; set; }
        public string Type { get; set; }
        public string State { get; set; }
        public string PublicIP { get; set; }
        public string Health { get; set; }
        public Geometry HealthIconPath
        {
            get
            {
                return Health switch
                {
                    "Healthy" =>  Geometry.Parse("M20.274001,10.542006L21.570001,12.055005 11.479,20.674995 7.5810001,15.146002 9.207,13.998003 11.849,17.740999z M13.866527,2.2019958C10.048523,6.8069916 4.1854995,8.7969971 2.1014939,9.3860016 1.776496,12.313995 1.3194939,24.153 13.981532,29.865997 26.234559,24.259995 26.195571,13.018005 25.890563,9.673996 25.876571,9.5139923 25.767562,9.3919983 25.60057,9.348999 24.861568,9.1640015 18.342538,7.4129944 13.866527,2.2019958z M13.845531,0L13.86053,0C14.351528,0 14.821532,0.23100281 15.152541,0.63600159 19.22755,5.5890045 25.396558,7.2460022 26.085571,7.4169922 27.079562,7.6679993 27.781575,8.4810028 27.874578,9.4909973 28.215568,13.209 28.254569,25.806 14.485532,31.820999L14.078533,32 13.569529,31.863998C-1.1805132,25.416992 -0.19151202,11.595993 0.15449728,8.8899994 0.23849713,8.2339935 0.70448663,7.6990051 1.3444878,7.5279999 2.5705045,7.197998 8.8755152,5.3159943 12.549522,0.66299438 12.876534,0.24699402 13.349527,0.0059967041 13.845531,0z"),
                    "Warning" => Geometry.Parse("M15.998999,17.988002C16.551,17.988002 16.999,18.437009 16.999,18.990018 16.999,19.543027 16.551,19.992034 15.998999,19.992034 15.446999,19.992034 14.998999,19.543027 14.998999,18.990018 14.998999,18.437009 15.446999,17.988002 15.998999,17.988002z M15.998999,8.0630169C16.552,8.0630169,16.999,8.5110159,16.999,9.0650149L16.999,16.167001C16.999,16.721 16.552,17.168998 15.998999,17.168998 15.446,17.168998 14.998999,16.721 14.998999,16.167001L14.998999,9.0650149C14.998999,8.5110159,15.446,8.0630169,15.998999,8.0630169z M15.999277,2.3337629L2.3830507,22.996989 29.615442,22.996989z M15.999513,0C16.50052,-3.8678991E-07,17.001274,0.21826346,17.288297,0.65479128L31.74151,22.585975C32.057495,23.064982 32.084472,23.677966 31.812494,24.184989 31.541494,24.687009 31.02049,24.999999 30.450475,24.999999L1.5480773,24.999999C0.97903845,24.999999 0.45705819,24.687009 0.18703459,24.184989 -0.085979476,23.677966 -0.057963934,23.064982 0.25704309,22.585975L14.709219,0.65479128C14.997247,0.21826346,15.498506,-3.8678991E-07,15.999513,0z"), // Yellow triangle
                    "Critical" => Geometry.Parse("M26.580956,6.8329883L6.8329535,26.581059 6.8670793,26.611267C9.4062033,28.804157 12.611519,30 16,30 19.738998,30 23.25499,28.543945 25.898987,25.898926 28.542999,23.255005 30,19.739014 30,16 30,12.611519 28.803381,9.4062033 26.611095,6.8670454z M16,2C12.260986,2 8.7449951,3.4559326 6.1009979,6.1009521 3.4570007,8.7449951 2,12.260986 2,16 2,19.388481 3.1966188,22.593796 5.3888941,25.132864L5.4190583,25.166948 25.167076,5.4188623 25.132906,5.3886132C22.593781,3.1957417,19.388468,2,16,2z M16,0C20.272995,0 24.291992,1.6639404 27.312988,4.6870117 30.334991,7.7080078 32,11.726929 32,16 32,20.272949 30.334991,24.291992 27.312988,27.312988 24.291992,30.335938 20.272995,32 16,32 11.72699,32 7.7079926,30.335938 4.6869965,27.312988 1.6649933,24.291992 0,20.272949 0,16 0,11.726929 1.6649933,7.7080078 4.6869965,4.6870117 7.7079926,1.6639404 11.72699,0 16,0z"),
                    _ => Geometry.Parse("")
                };
            }
        }
        public Brush HealthColor
        {
            get
            {
                return Health switch
                {
                    "Healthy" => new SolidColorBrush(Color.FromArgb(255, 40, 201, 55)),
                    "Warning" => new SolidColorBrush(Color.FromArgb(255, 255, 157, 0)),
                    "Critical" => new SolidColorBrush(Color.FromArgb(255, 229, 0, 0)),
                    _ => Brushes.Gray
                };
            }
        }
        public Brush HealthBackground
        {
            get
            {
                return Health switch
                {
                    "Healthy" => new SolidColorBrush(Color.FromArgb(255, 214, 246, 217)),
                    "Warning" => new SolidColorBrush(Color.FromArgb(255, 255, 238, 211)),
                    "Critical" => new SolidColorBrush(Color.FromArgb(255, 255, 201, 201)),
                    _ => Brushes.Gray
                };
            }
        }

        public InstanceInfo(string instanceID, string type, string publicIP, string state, string health)
        {
            InstanceID = instanceID;
            State = type;
            PublicIP = publicIP;
            Type = state;
            Health = health;
        }
    }
}
