﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.unite
{
    public partial class Fraction
    {
        /// <summary>
        ///     Performs an implicit conversion from <see cref="Fraction" /> to <see cref="MassConcentrationInWater" />.
        /// </summary>
        /// <param name="c"> The fraction. </param>
        /// <returns> MassConcentration. </returns>
        public static implicit operator MassConcentrationInWater(Fraction f)
        {
            // Base unit of MassConcentrationInWater is kg/m^3. 1mg/L == 1 ppm.
            return MassConcentrationInWater.MilligramPerLitre * f.ConvertTo(Fraction.PartPerMillion);
        }

        /// <summary>
        ///     Performs an implicit conversion from <see cref="Fraction" /> to <see cref="VolumetricWaterContent" />.
        /// </summary>
        /// <param name="c"> The fraction. </param>
        /// <returns> VolumetricWaterContent. </returns>
        public static implicit operator VolumetricWaterContent(Fraction f)
        {
            // Base unit of VolumetricWaterContent m^3/m^3.
            return new VolumetricWaterContent(f.value);
        }

    }
}
