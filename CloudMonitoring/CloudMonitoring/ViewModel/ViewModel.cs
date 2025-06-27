using Syncfusion.UI.Xaml.Charts;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Threading;

namespace CloudMonitoring
{
    public class ViewModel : INotifyPropertyChanged
    {
        private string[] diskusageItems = ["Downloads", "Documents", "RootGB", "MediaGB"];
        private const int MaxPoints = 10;

        private ObservableCollection<RealTimeChartModel> realtimechartData = new();
        public ObservableCollection<RealTimeChartModel> RealTimeChartData
        {
            get { return realtimechartData; }
            set
            {
                if(realtimechartData != value)
                {
                    realtimechartData = value;
                    OnPropertyChanged(nameof(RealTimeChartData));
                }
            }
        }

        private ObservableCollection<CircularChartModel> cpuDatasets = new();
        public ObservableCollection<CircularChartModel> CPUDatasets
        {
            get { return cpuDatasets; }
            set
            {
                if(cpuDatasets != value)
                {
                    cpuDatasets = value;
                    OnPropertyChanged(nameof(CPUDatasets));
                }
            }
        }

        private ObservableCollection<CircularChartModel> memoryDatasets = new();
        public ObservableCollection<CircularChartModel> MemoryDatasets
        {
            get { return memoryDatasets; }
            set
            {
                if(memoryDatasets != value)
                {
                    memoryDatasets = value; OnPropertyChanged(nameof(MemoryDatasets));
                }
            }
        }

        private ObservableCollection<CircularChartModel>diskspaceData = new();
        public ObservableCollection<CircularChartModel>DiskSpaceData
        {
            get { return diskspaceData; }
            set
            {
                if(diskspaceData != value)
                {
                    diskspaceData = value; OnPropertyChanged(nameof(DiskSpaceData));
                }
            }
        }

        private ObservableCollection<DiskUsageModel> diskUsageDatasets = new();
        public ObservableCollection<DiskUsageModel> DiskUsageDatasets
        {
            get { return diskUsageDatasets; }
            set
            {
                if( diskUsageDatasets != value)
                {
                    diskUsageDatasets = value; OnPropertyChanged(nameof(DiskUsageDatasets));
                }
            }
        }

        internal DispatcherTimer dispatcherTimer;
        private Random _random = new Random();

        private ObservableCollection<InstanceInfo> _instanceList;
        public ObservableCollection<InstanceInfo> InstanceList
        {
            get { return _instanceList; }
            set { _instanceList = value; }
        }

        private string cputext;
        public string CPUText
        {
            get { return  cputext; }
            set
            {
                if(cputext != value)
                {
                    cputext = value;
                    OnPropertyChanged(nameof(CPUText));
                }
            }
        }

        private string memoryText;
        public string MemoryText
        {
            get { return memoryText; }
            set
            {
                if (memoryText != value)
                {
                    memoryText = value;
                    OnPropertyChanged(nameof(MemoryText));
                }
            }
        }

        private string diskText;
        public string DiskText
        {
            get { return diskText; }
            set
            {
                if (diskText != value)
                {
                    diskText = value;
                    OnPropertyChanged(nameof(DiskText));
                }
            }
        }

        public ViewModel()
        {
            InstanceList = GetInstanceDataCollection();

            RealTimeChartData = new ObservableCollection<RealTimeChartModel>();
            
            CPUDatasets = new ObservableCollection<CircularChartModel>
            {
                 new CircularChartModel { Name = "Used", Value = 40 },
                 new CircularChartModel { Name = "Free", Value = 60 }
            };

            CPUText = 40.15.ToString() + "%";

            MemoryDatasets = new ObservableCollection<CircularChartModel>
            {
                 new CircularChartModel { Name = "Used", Value = 24 },
                 new CircularChartModel { Name = "Free", Value = 1000 }
            };

            MemoryText = 24.35.ToString() + " MB";

            DiskSpaceData = new ObservableCollection<CircularChartModel>
            {
                 new CircularChartModel { Name = "Used", Value = 25 },
                 new CircularChartModel { Name = "Free", Value = 75 }
            };

            DiskText = 25.ToString() + " GB";

            DiskUsageDatasets = new ObservableCollection<DiskUsageModel>()
            {
                new DiskUsageModel{ Name = "Downloads", Value = 15},
                new DiskUsageModel{ Name = "Documents", Value = 5},
                new DiskUsageModel{ Name = "RootGB", Value = 5},
                new DiskUsageModel{ Name = "MediaGB", Value = 5}
            };

            for (int i = 0; i < MaxPoints; i++)
            {
                RealTimeChartData.Add(new RealTimeChartModel
                {
                    Time = DateTime.Now.AddSeconds(i - MaxPoints),
                    CPUUsage = _random.Next(10, 85),
                    NetworkIn = _random.Next(10, 85),
                    NetworkOut = _random.Next(20, 70),
                    Read = _random.Next(10, 80),
                    Write = _random.Next(10, 95)
                });
            }

            dispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2)
            };
            dispatcherTimer.Tick += UpdateData;
            dispatcherTimer.Start();

        }

        private void UpdateData(object sender, EventArgs e)
        {
            double cpuUsed = _random.Next(0, 100);
            double cpuFree = 100 - cpuUsed;
            
            CPUDatasets.Clear();
            CPUDatasets.Add(new CircularChartModel() { Name = "Used", Value = cpuUsed });
            CPUDatasets.Add(new CircularChartModel() { Name = "Free", Value = cpuFree });
            CPUText = cpuUsed.ToString() + "%";

            double memoryUsed = _random.Next(20, 100);
            double memoryFree = 1024 - memoryUsed;

            MemoryDatasets.Clear();
            MemoryDatasets.Add(new CircularChartModel() { Name = "Used", Value = memoryUsed });
            MemoryDatasets.Add(new CircularChartModel() { Name = "Free", Value = memoryFree });
            MemoryText = memoryUsed.ToString() + " MB";

            double diskUsed = 0;
            
            DiskUsageDatasets.Clear();
            foreach (var item in diskusageItems)
            {
                double value = _random.Next(5, 15);
                DiskUsageDatasets.Add(new DiskUsageModel() { Name = item, Value = value });
                diskUsed += value;
            }

            DiskText = diskUsed.ToString() + " GB";
            double diskFree = 100 - diskUsed;

            DiskSpaceData.Clear();
            DiskSpaceData.Add(new CircularChartModel() { Name = "Used", Value = diskUsed });
            DiskSpaceData.Add(new CircularChartModel() { Name = "Free", Value = diskFree });

            UpdateRealTimeData();
        }

        private void UpdateRealTimeData()
        {
            if (RealTimeChartData.Count >= MaxPoints)
                RealTimeChartData.RemoveAt(0);

            RealTimeChartData.Add(new RealTimeChartModel
            {
                Time = DateTime.Now,
                CPUUsage = _random.Next(10, 85),
                NetworkIn = _random.Next(10, 85),
                NetworkOut = _random.Next(20, 70),
                Read = _random.Next(10, 80),
                Write = _random.Next(10, 95)
            });
        }

        private ObservableCollection<InstanceInfo> GetInstanceDataCollection()
        {
            return new ObservableCollection<InstanceInfo>()
            {
                new InstanceInfo ( "i-0abcdef1234567890","t2.micro","54.123.45.67","running","Healthy" ),
                new InstanceInfo ("i-1a2b3c4d5e678901", "t3.small", "354.123.45.67", "running", "Warning"),
                new InstanceInfo ("i-9876543210abcdef0", "m5.large", "54.321.67.89", "stopped", "Critical"),
                new InstanceInfo ("i-12345678ghijklm0", "t5.micro", "57.012.77.45", "running", "Warning"),
                new InstanceInfo ("i-237891tyvdhykio0", "m3.small", "245.237.89.45", "running", "Healthy"),
                new InstanceInfo ("i-1447852mnbvcxzasdf0", "t1.small", "165.654.78.32", "running", "Critical")
            };
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
