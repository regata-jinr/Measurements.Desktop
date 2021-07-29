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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Regata.Desktop.WinForms.Measurements
{
    public partial class Session : IDisposable
    {
        public void CreateMeasurementsRegister(string name, string type)
        {
            // TODO: add record to db and dictionary
            // TODO: check if register with such pair of name and type already exist in this case add _n to the end of name
        }
        public void RenameMeasurementsRegister(string oldName, string newName)
        {
            // TODO: change record in db and dictionary
        }
        public async Task AddSampleToRegisterAsync(Measurement mi)
        {
            MeasurementsRegisters[_activeMeasurementsRegister].Add(mi);
        }

        public void AddSampleSRangeToRegisterAsync(IEnumerable<Measurement> mis)
        {
            MeasurementsRegisters[_activeMeasurementsRegister].AddRange(mis);
        }

        public async Task CopyMeasurementsRegisterAsync(string mName, string newName)
        {
            
        }
        public async Task RemoveMeasurementsRegisterAsync(string mName)
        {
            
        }

        public async Task ExportToExcelAsync(string mName)
        {
            
        }

    }
}


