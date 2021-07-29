/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2017-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Regata.Core.DB.MSSQL.Models;

namespace Regata.Measurements.Managers
{
    public static partial class AppManager
    {
        public static event Action AppGoToLocalMode;
        public static event Action AppLeaveLocalMode;

        private static bool _localMode;
        public static bool LocalMode
        {
            get
            {
                return _localMode;
            }
            private set
            {
                _localMode = value;
                if (value)
                    AppGoToLocalMode?.Invoke();
                else
                    AppLeaveLocalMode?.Invoke();
            }
        }

        /// <summary>
        /// This internal method will be call when ConnectionRestoreEvent will occur <see cref="SessionControllerSingleton.ConectionRestoreEvent"/>
        /// It upload all files into memory via usage of desirilizer and then upload it to database.
        /// </summary>
        /// <returns>List of object with Measurement type that will be load to the data base. <seealso cref="Measurement"/></returns>
        private static List<Measurement> LoadMeasurementsFiles()
        {
            var MeasurementsInfoForUpload = new List<Measurement>();
            try
            {
                //logger.Info($"Deserilization informataion inside 'D:\\MeasurementsLocalData'  has begun");
                var dir = new DirectoryInfo(@"D:\MeasurementsLocalData");

                if (!dir.Exists)
                    return MeasurementsInfoForUpload;

                var files = dir.GetFiles("*.json").ToList();
                var options = new JsonSerializerOptions();
                options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                foreach (var file in files)
                {
                    //logger.Info($"Deserilization informataion from the file '{file.Name}'");
                    MeasurementsInfoForUpload.Add(JsonSerializer.Deserialize<Measurement>(File.ReadAllText(file.FullName), options));
                }
                //logger.Info($"Deserilization informataion inside 'D:\\MeasurementsLocalData'  has done");
            }
            catch (Exception e)
            {
                Report.Notify();
            }

            return MeasurementsInfoForUpload;
        }

        public static bool CheckLocalStorage()
        {
            throw new NotImplementedException();
        }

    } // public static partial class AppManager
} // namespace Regata.Measurements.Managers
