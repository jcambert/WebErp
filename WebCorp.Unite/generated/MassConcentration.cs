 
// --------------------------------------------------------------------------------------------------------------------
// <auto-generated> 
//   This code was generated by a T4 template. 
//
//   Changes to this file may cause incorrect behavior and will be lost if 
//   the code is regenerated. 
// </auto-generated>
// <copyright file="MassConcentration.cs" company="Webcorp">
//   Copyright (c) 2015 Webcorp contributors
// </copyright>
// <summary>
//   Represents the mass concentration quantity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Webcorp.unite
{
    using System;
	using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
	using System.Collections.Generic;
	using MongoDB.Bson.Serialization.Attributes;
	using MongoDB.Bson.Serialization.Serializers;
    using MongoDB.Bson.Serialization;

    /// <summary>
    /// Represents the mass concentration quantity.
    /// </summary>
    [DataContract]
#if !PCL
    [Serializable]
    [TypeConverter(typeof(UnitTypeConverter<MassConcentration>))]
#endif
    public partial class MassConcentration : Unit<MassConcentration>
    {
        /// <summary>
        /// The backing field for the <see cref="KilogramPerCubicMetre" /> property.
        /// </summary>
		[Unit("kg/m^3", true)]
		private static readonly MassConcentration KilogramPerCubicMetreField = new MassConcentration(1);

        /// <summary>
        /// The backing field for the <see cref="GramPerCubicCentimetre" /> property.
        /// </summary>
		[Unit("g/cm^3")]
		private static readonly MassConcentration GramPerCubicCentimetreField = new MassConcentration(1e3);

        /// <summary>
        /// The backing field for the <see cref="KilogramPerLitre" /> property.
        /// </summary>
		[Unit("kg/L")]
		private static readonly MassConcentration KilogramPerLitreField = new MassConcentration(1e3);

        /// <summary>
        /// The backing field for the <see cref="MilligramPerLitre" /> property.
        /// </summary>
		[Unit("mg/L")]
		private static readonly MassConcentration MilligramPerLitreField = new MassConcentration(1e-3);

        /// <summary>
        /// The backing field for the <see cref="MicrogramPerLitre" /> property.
        /// </summary>
		[Unit("ug/L")]
		private static readonly MassConcentration MicrogramPerLitreField = new MassConcentration(1e-6);

        /// <summary>
        /// The backing field for the <see cref="GramPerMillilitre" /> property.
        /// </summary>
		[Unit("g/mL")]
		private static readonly MassConcentration GramPerMillilitreField = new MassConcentration(1e3);

		private readonly List<string> registeredSymbols;

		public override List<string> RegisteredSymbols=>registeredSymbols;
        /// <summary>
        /// The value.
        /// </summary>
        private double value;

		/// <summary>
        /// Initializes a new instance of the <see cref="MassConcentration"/> struct.
        /// </summary>
        public MassConcentration():this(0.0)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MassConcentration"/> struct.
        /// </summary>
        /// <param name="value">
        /// The value. 
        /// </param>
        public MassConcentration(double value)
        {
            this.value = value;
			registeredSymbols = new List<string>() { "kg/m^3","g/cm^3","kg/L","mg/L","ug/L","g/mL"};
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MassConcentration"/> struct.
        /// </summary>
        /// <param name="value">
        /// The value. 
        /// </param>
        /// <param name="unitProvider">
        /// The unit provider. 
        /// </param>
        public MassConcentration(string value, IUnitProvider unitProvider = null)
        {
            this.value = Parse(value, unitProvider ?? UnitProvider.Default).value;
			registeredSymbols = new List<string>() { "kg/m^3","g/cm^3","kg/L","mg/L","ug/L","g/mL"};
        }

        /// <summary>
        /// Gets the "kg/m^3" unit.
        /// </summary>
		[Unit("kg/m^3", true)]
		        public static MassConcentration KilogramPerCubicMetre
        {
            get { return KilogramPerCubicMetreField; }
        }

        /// <summary>
        /// Gets the "g/cm^3" unit.
        /// </summary>
		[Unit("g/cm^3")]
		        public static MassConcentration GramPerCubicCentimetre
        {
            get { return GramPerCubicCentimetreField; }
        }

        /// <summary>
        /// Gets the "kg/L" unit.
        /// </summary>
		[Unit("kg/L")]
		        public static MassConcentration KilogramPerLitre
        {
            get { return KilogramPerLitreField; }
        }

        /// <summary>
        /// Gets the "mg/L" unit.
        /// </summary>
		[Unit("mg/L")]
		        public static MassConcentration MilligramPerLitre
        {
            get { return MilligramPerLitreField; }
        }

        /// <summary>
        /// Gets the "ug/L" unit.
        /// </summary>
		[Unit("ug/L")]
		        public static MassConcentration MicrogramPerLitre
        {
            get { return MicrogramPerLitreField; }
        }

        /// <summary>
        /// Gets the "g/mL" unit.
        /// </summary>
		[Unit("g/mL")]
		        public static MassConcentration GramPerMillilitre
        {
            get { return GramPerMillilitreField; }
        }

        /// <summary>
        /// Gets or sets the mass concentration as a string.
        /// </summary>
        /// <value>The string.</value>
        /// <remarks>
        /// This property is used for XML serialization.
        /// </remarks>
        //[XmlText]
        [DataMember]
		//[BsonSerializer(typeof(UnitSerializer))]
		//[BsonSerializer(typeof(MassConcentrationSerializer))]
        public string FValue
        {
            get
            {
                // Use round-trip format
                return this.ToString("R", CultureInfo.InvariantCulture);
            }

            set
            {
                this.value = Parse(value, CultureInfo.InvariantCulture).value;
            }
        }

        /// <summary>
        /// Gets the value of the mass concentration in the base unit.
        /// </summary>
        public override double Value
        {
            get
            {
                return this.value;
            }
        }

        /// <summary>
        /// Converts a string representation of a quantity in a specific culture-specific format with a specific unit provider.
        /// </summary>
        /// <param name="input">
        /// A string that contains the quantity to convert. 
        /// </param>
        /// <param name="provider">
        /// An object that supplies culture-specific formatting information about <paramref name="input" />. If not specified, the culture of the default <see cref="UnitProvider" /> is used. 
        /// </param>
        /// <param name="unitProvider">
        /// The unit provider. If not specified, the default <see cref="UnitProvider" /> is used.
        /// </param>
        /// <returns>
        /// A <see cref="MassConcentration"/> that represents the quantity in <paramref name="input" />. 
        /// </returns>
        public static MassConcentration Parse(string input, IFormatProvider provider, IUnitProvider unitProvider)
        {
            if (unitProvider == null)
            {
                unitProvider = provider as IUnitProvider ?? UnitProvider.Default;
            }

            MassConcentration value;
            if (!unitProvider.TryParse(input, provider, out value))
            {
				return new  MassConcentration(0);
                //throw new FormatException("Invalid format.");
            }

            return value;
        }

        /// <summary>
        /// Converts a string representation of a quantity in a specific culture-specific format.
        /// </summary>
        /// <param name="input">
        /// A string that contains the quantity to convert. 
        /// </param>
        /// <param name="provider">
        /// An object that supplies culture-specific formatting information about <paramref name="input" />. If not specified, the culture of the default <see cref="UnitProvider" /> is used. 
        /// </param>
        /// <returns>
        /// A <see cref="MassConcentration"/> that represents the quantity in <paramref name="input" />. 
        /// </returns>
        public static MassConcentration Parse(string input, IFormatProvider provider = null)
        {
            var unitProvider = provider as IUnitProvider ?? UnitProvider.Default;

            MassConcentration value;
            if (!unitProvider.TryParse(input, provider, out value))
            {
				return new  MassConcentration(0);
                //throw new FormatException("Invalid format.");
            }

            return value;
        }

        /// <summary>
        /// Converts a string representation of a quantity with a specific unit provider.
        /// </summary>
        /// <param name="input">
        /// A string that contains the quantity to convert. 
        /// </param>
        /// <param name="unitProvider">
        /// The unit provider. If not specified, the default <see cref="UnitProvider" /> is used.
        /// </param>
        /// <returns>
        /// A <see cref="MassConcentration"/> that represents the quantity in <paramref name="input" />. 
        /// </returns>
        public static MassConcentration Parse(string input, IUnitProvider unitProvider)
        {
            if (unitProvider == null)
            {
                unitProvider = UnitProvider.Default;
            }

            MassConcentration value;
            if (!unitProvider.TryParse(input, unitProvider.Culture, out value))
            {
				return new  MassConcentration(0);
                //throw new FormatException("Invalid format.");
            }

            return value;
        }

        /// <summary>
        /// Tries to parse the specified string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="provider">The format provider.</param>
        /// <param name="unitProvider">The unit provider.</param>
        /// <param name="result">The result.</param>
        /// <returns><c>true</c> if the string was parsed, <c>false</c> otherwise.</returns>
        public static bool TryParse(string input, IFormatProvider provider, IUnitProvider unitProvider, out MassConcentration result)
        {
            if (unitProvider == null)
            {
                unitProvider = provider as IUnitProvider ?? UnitProvider.Default;
            }

            return unitProvider.TryParse(input, provider, out result);
        }

        /// <summary>
        /// Parses the specified JSON string.
        /// </summary>
        /// <param name="input">The JSON input.</param>
        /// <returns>
        /// The <see cref="MassConcentration"/> .
        /// </returns>
        public static MassConcentration ParseJson(string input)
        {
            return Parse(input, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="x">
        /// The first value. 
        /// </param>
        /// <param name="y">
        /// The second value. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static MassConcentration operator +(MassConcentration x, MassConcentration y)
        {
            return new MassConcentration(x.value + y.value);
        }

        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <param name="y">
        /// The y. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static MassConcentration operator /(MassConcentration x, double y)
        {
            return new MassConcentration(x.value / y);
        }

        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <param name="y">
        /// The y. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static double operator /(MassConcentration x, MassConcentration y)
        {
            return x.value / y.value;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <param name="y">
        /// The y. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static bool operator ==(MassConcentration x, MassConcentration y)
        {
            return x.value.Equals(y.value);
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <param name="y">
        /// The y. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static bool operator >(MassConcentration x, MassConcentration y)
        {
            return x.value > y.value;
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <param name="y">
        /// The y. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static bool operator >=(MassConcentration x, MassConcentration y)
        {
            return x.value >= y.value;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <param name="y">
        /// The y. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static bool operator !=(MassConcentration x, MassConcentration y)
        {
            return !x.value.Equals(y.value);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <param name="y">
        /// The y. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static bool operator <(MassConcentration x, MassConcentration y)
        {
            return x.value < y.value;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <param name="y">
        /// The y. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static bool operator <=(MassConcentration x, MassConcentration y)
        {
            return x.value <= y.value;
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <param name="y">
        /// The y. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static MassConcentration operator *(double x, MassConcentration y)
        {
            return new MassConcentration(x * y.value);
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <param name="y">
        /// The y. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static MassConcentration operator *(MassConcentration x, double y)
        {
            return new MassConcentration(x.value * y);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <param name="y">
        /// The y. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static MassConcentration operator -(MassConcentration x, MassConcentration y)
        {
            return new MassConcentration(x.value - y.value);
        }

        /// <summary>
        /// Implements the unary plus operator.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static MassConcentration operator +(MassConcentration x)
        {
            return new MassConcentration(x.value);
        }

        /// <summary>
        /// Implements the unary minus operator.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static MassConcentration operator -(MassConcentration x)
        {
            return new MassConcentration(-x.value);
        }

        /// <summary>
        /// Compares this instance to the specified <see cref="MassConcentration"/> and returns an indication of their relative values.
        /// </summary>
        /// <param name="other">
        /// The other <see cref="MassConcentration"/> . 
        /// </param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and the other value. 
        /// </returns>
        public override int CompareTo(MassConcentration other)
        {
            return this.value.CompareTo(other.value);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the 
        /// current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: 
        /// Value Meaning Less than zero This instance is less than <paramref name="obj" />. Zero This instance is equal to 
        /// <paramref name="obj" />. Greater than zero This instance is greater than <paramref name="obj" />.
        /// </returns>
        public override int CompareTo(object obj)
        {
            return this.CompareTo((MassConcentration)obj);
        }

        /// <summary>
        /// Converts the quantity to the specified unit.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <returns>The amount of the specified unit.</returns>
		public override double ConvertTo(IUnit unit)
        {
            return this.ConvertTo((MassConcentration)unit);
        }

        /// <summary>
        /// Converts to the specified unit.
        /// </summary>
        /// <param name="unit">
        /// The unit. 
        /// </param>
        /// <returns>
        /// The value in the specified unit. 
        /// </returns>
        public double ConvertTo(MassConcentration unit)
        {
            return this.value / unit.Value;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="System.Object"/> to compare with this instance. 
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c> . 
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is MassConcentration)
            {
                return this.Equals((MassConcentration)obj);
            }

            return false;
        }

        /// <summary>
        /// Determines if the specified <see cref="MassConcentration"/> is equal to this instance.
        /// </summary>
        /// <param name="other">
        /// The other <see cref="MassConcentration"/> . 
        /// </param>
        /// <returns>
        /// True if the values are equal. 
        /// </returns>
        public override bool Equals(MassConcentration other)
        {
            return this.value.Equals(other.value);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        /// <summary>
        /// Multiplies by the specified number.
        /// </summary>
        /// <param name="x">The number.</param>
        /// <returns>The new quantity.</returns>
        public override IUnit MultiplyBy(double x)
        {
            return this * x;
        }

        /// <summary>
        /// Adds the specified quantity.
        /// </summary>
        /// <param name="x">The quantity to add.</param>
        /// <returns>The sum.</returns>
        public override IUnit Add(IUnit x)
        {
            if (!(x is MassConcentration))
            {
                throw new InvalidOperationException("Can only add quantities of the same types.");
            }

            return new MassConcentration(this.value + x.Value);
        }

        /// <summary>
        /// Sets the value from the specified string.
        /// </summary>
        /// <param name="s">
        /// The s. 
        /// </param>
        /// <param name="provider">
        /// The provider. 
        /// </param>
        public void SetFromString(string s, IUnitProvider provider)
        {
            this.value = Parse(s, provider).value;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance. 
        /// </returns>
        public override string ToString()
        {
            return this.ToString(null, UnitProvider.Default);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <param name="formatProvider">
        /// The format provider. 
        /// </param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance. 
        /// </returns>
        public string ToString(IFormatProvider formatProvider)
        {
            var unitProvider = formatProvider as IUnitProvider ?? UnitProvider.Default;

            return this.ToString(null, formatProvider, unitProvider);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <param name="format">
        /// The format. 
        /// </param>
        /// <param name="formatProvider">
        /// The format provider. 
        /// </param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance. 
        /// </returns>
        public override string ToString(string format, IFormatProvider formatProvider = null)
        {
            var unitProvider = formatProvider as IUnitProvider ?? UnitProvider.Default;

            return this.ToString(format, formatProvider, unitProvider);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <param name="format">
        /// The format. 
        /// </param>
        /// <param name="formatProvider">
        /// The format provider. 
        /// </param>
        /// <param name="unitProvider">
        /// The unit provider. 
        /// </param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance. 
        /// </returns>
        public  string ToString(string format, IFormatProvider formatProvider, IUnitProvider unitProvider)
        {
            if (unitProvider == null)
            {
                unitProvider = formatProvider as IUnitProvider ?? UnitProvider.Default;
            }

            return unitProvider.Format(format, formatProvider, this);
        }
    }

	public class MassConcentrationSerializer:SerializerBase<MassConcentration>{
		public override MassConcentration Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var up = UnitProvider.Default;
            IUnit result;
            if(up.TryGetUnit(typeof(MassConcentration), context.Reader.ReadString(), out result))
                return (MassConcentration)result;

            return base.Deserialize(context, args);
        } 
	}
}
