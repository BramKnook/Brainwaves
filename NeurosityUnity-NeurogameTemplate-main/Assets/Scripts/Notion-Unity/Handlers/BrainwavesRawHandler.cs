﻿using Newtonsoft.Json;
using System.Text;
using System;
using System.Linq;
using UnityEngine;

namespace Notion.Unity
{
    public class BrainwavesRawHandler : IMetricHandler
    {
        public Metrics Metric => Metrics.Brainwaves;
        public string Label => "raw";

        public Action<Epoch> OnBrainwavesRawUpdated { get; set; }

        private readonly StringBuilder _builder;

        public BrainwavesRawHandler()
        {
            _builder = new StringBuilder();
        }

        public void Handle(string metricData)
        {
            Epoch epoch = JsonConvert.DeserializeObject<Epoch>(metricData);

            OnBrainwavesRawUpdated?.Invoke(epoch);

            _builder.AppendLine("Handling Raw Brainwaves")
                .Append("Label: ").AppendLine(epoch.Label)
                .Append("Notch Frequency: ").AppendLine(epoch.Info.NotchFrequency)
                .Append("Sampling Rate: ").AppendLine(epoch.Info.SamplingRate.ToString())
                .Append("Star Time: ").AppendLine(epoch.Info.StartTime.ToString())
                .Append("Channel Names: ").AppendLine(string.Join(", ", epoch.Info.ChannelNames));

            //Debug.Log(_builder.ToString());
            _builder.Clear();
        }
    }
}