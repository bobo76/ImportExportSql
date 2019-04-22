using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

namespace ImportExportSql
{
    // Kin has a free form text field.  Under happy path, we break out form / route / frequency when known and safe.
    // This is a helper class to do this.  FinalValue, is FinalValue (eg, PRN).
    public class ReplacementString
    {
        public List<string> PossibleValues { get; }
        public string FinalValue { get; }
        public ReplacementString(string finalValue, string[] possibleValues = null)
        {
            FinalValue = finalValue;
            PossibleValues = new List<string>();
            if (possibleValues != null)
                PossibleValues.AddRange(possibleValues);
            if (!PossibleValues.Contains(finalValue))
                PossibleValues.Insert(0, finalValue);
        }

        public string TryReplace(string value, Action<string> setOutput)
        {
            foreach (var element in PossibleValues)
            {
                if (ContainsCaseInsensitive(value, element))
                {
                    setOutput(Regex.Replace(value, Regex.Escape(element), "", RegexOptions.IgnoreCase).Replace("  ", " ").Trim());
                    return FinalValue;
                }
            }

            return null;
        }

        private static bool ContainsCaseInsensitive(string source, string value, StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase)
        {
            return source.IndexOf(value, stringComparison) >= 0;
        }
    }
    public class DrugV
    {
        public int CodeDIN { get; set; }
        public string CodeGEN { get; set; }
        public string BrandName { get; set; }
        public string Strength { get; set; }
        public string Form { get; set; }
        public string VendorCode { get; set; }
        public bool RAMQIndicator { get; set; }
        public decimal Price { get; set; }
        public string Nature { get; set; }
        public string Monography { get; set; }
        public string Leaflet { get; set; }
        public string InfoSupp { get; set; }
        public bool Active { get; set; }
        public string GenericName { get; set; }
        public List<DrugFormula> Formulas { get; set; }
        public bool UserFavourite { get; set; }

        public DrugV()
        {
        }

        public DrugV(SqlDataReader dr)
        {
            CodeDIN = DBConverters.GetNullableInt32Number(dr, "CodeDIN");
            CodeGEN = DBConverters.GetNullableString(dr, "CodeGEN");
            BrandName = DBConverters.GetNullableString(dr, "BrandName");
            Strength = DBConverters.GetNullableString(dr, "Strength");
            Form = DBConverters.GetNullableString(dr, "Form");
            RAMQIndicator = DBConverters.GetNullableString(dr, "RAMQIndicator").ToLower() == "o"; //O or N
            Price = DBConverters.GetNullableGetDecimalOrDefault(dr, "Price");
            Nature = DBConverters.GetNullableString(dr, "Nature");
            Monography = DBConverters.GetNullableString(dr, "Monography");
            Leaflet = DBConverters.GetNullableString(dr, "Leaflet");
            InfoSupp = DBConverters.GetNullableString(dr, "InfoSupp");
            Active = DBConverters.GetNullableBoolean(dr, "Active");
            GenericName = DBConverters.GetNullableString(dr, "GenericName");
        }

        public override string ToString()
        {
            var ToReturn = BrandName.Trim();
            if (!string.IsNullOrWhiteSpace(Strength))
                ToReturn += " " + Strength.Trim();
            if (!string.IsNullOrWhiteSpace(Form))
                ToReturn += " " + Form.Trim();

            return ToReturn;
        }

        public bool HasException
        {
            get
            {
                return !string.IsNullOrEmpty(InfoSupp);
            }
        }
    }
    public class DrugFormula
    {
        public string Id { get; set; }
        public string Number { get; set; }
        public string NumberO { get; set; }
        public string Administration { get; set; }
        public string AdministrationO { get; set; }
        public string Duration { get; set; }
        public string DurationO { get; set; }
        public string Quantity { get; set; }
        public string QuantityO { get; set; }
        public string Repetatur { get; set; }
        public int CodeDIN { get; set; }
        public string OriginalText { get; }
        public bool UserTemplate { get; set; }
        public string BrandName { get; }
        public string RouteFinal { get; set; }
        public string FrequencyFinal { get; set; }
        public string DurationFinal { get; set; }
        public string NumberFinal { get; set; }
        public string QuantityFinal { get; set; }
        public bool Prn { get; set; }
        public bool IsValid { get; set; }


        public DrugFormula()
        {
        }

        public DrugFormula(SqlDataReader dr, string idPrefix)
        {
            Id = idPrefix + DBConverters.GetNullableInt32Number(dr, "Id");
            CodeDIN = DBConverters.GetNullableInt32Number(dr, "CodeDIN");
            Number = DBConverters.GetNullableString(dr, "Number");
            NumberO = Number;
            Administration = DBConverters.GetNullableString(dr, "Administration");
            AdministrationO = Administration;
            Duration = DBConverters.GetNullableString(dr, "Duration");
            DurationO = Duration;
            Quantity = DBConverters.GetNullableString(dr, "Quantity");
            QuantityO = Quantity;
            Repetatur = DBConverters.GetNullableString(dr, "Repetatur");
            OriginalText = ToString();
            var source = DBConverters.GetNullableString(dr, "Source");
            UserTemplate = source == "Fav";
            BrandName = DBConverters.GetNullableString(dr, "BrandName");
        }

        public void UpdateCommand(SqlCommand cmd)
        {
            cmd.Parameters["@Id"].Value = Id;
            cmd.Parameters["@Number"].Value = ValueOrDbNull(NumberFinal);
            cmd.Parameters["@Administration"].Value = ValueOrDbNull(ToAdministration());
            cmd.Parameters["@Duration"].Value = ValueOrDbNull(DurationFinal);
            cmd.Parameters["@Quantity"].Value = ValueOrDbNull(QuantityFinal);
        }

        public string ToString()
        {
            var ToReturn = string.IsNullOrWhiteSpace(Number) ? string.Empty : Number.Trim() + " ";
            ToReturn += Administration.Trim();
            if (!string.IsNullOrWhiteSpace(Duration))
                ToReturn = string.IsNullOrEmpty(ToReturn) ? Duration.Trim() : ToReturn + " x " + Duration.Trim();
            return ToReturn;
        }

        public string ToAdministration()
        {
            var ToReturn = string.IsNullOrWhiteSpace(RouteFinal) ? string.Empty : RouteFinal.Trim() + " ";
            ToReturn += string.IsNullOrWhiteSpace(FrequencyFinal) ? string.Empty : FrequencyFinal.Trim() + " ";
            if (Prn)
                ToReturn += "p.r.n.";
            return ToReturn.Trim();
        }
        public static object ValueOrDbNull(object value)
        {
            if (value == null)
                return DBNull.Value;
            if (value is string && string.IsNullOrEmpty((string)value))
                return DBNull.Value;
            if (value is DateTime && (DateTime)value == DateTime.MinValue)
                return DBNull.Value;
            if (value is Guid && (Guid)value == Guid.Empty)
                return DBNull.Value;
            if (value is DateTimeOffset && (DateTimeOffset)value == DateTimeOffset.MinValue)
                return DBNull.Value;

            return value;
        }

    }

    public class DrugVSorter : IComparer<DrugV>
    {
        private string[] Filters { get; set; }
        public DrugVSorter(string[] filters)
        {
            Filters = filters;
        }

        public int Compare(DrugV x, DrugV y)
        {
            var comp = 0;
            // Priority to Ingrediant
            comp = CompareToFilter(x.GenericName, y.GenericName, Filters);
            if (comp == 0)// Then Brandname
                comp = CompareToFilter(x.BrandName, y.BrandName, Filters);
            if (comp == 0)// Then other fields
                comp = CompareToFilter(x.ToString(), y.ToString(), Filters);

            return comp != 0 ? comp : x.ToString().CompareTo(y.ToString());
        }

        private int CompareToFilter(string s1, string s2, string[] filter)
        {
            var sc = StringComparison.InvariantCultureIgnoreCase;
            // Priority to the first word
            if (s1.StartsWith(Filters[0], sc) && !s2.StartsWith(Filters[0], sc))
                return -1;
            else if (!s1.StartsWith(Filters[0], sc) && s2.StartsWith(Filters[0], sc))
                return 1;

            //Then, if thers is one, the second word
            if (filter.Length > 1)
            {
                if (s1.StartsWith(Filters[1], sc) && !s2.StartsWith(Filters[1], sc))
                    return -1;
                else if (!s1.StartsWith(Filters[1], sc) && s2.StartsWith(Filters[1], sc))
                    return 1;
            }
            return 0;
        }
    }

    public class DrugVEquality : IEqualityComparer<DrugV>
    {
        public bool Equals(DrugV x, DrugV y)
        {
            if (x.Active != y.Active)
                return false;
            if (x.BrandName != y.BrandName)
                return false;
            if (x.Form != y.Form)
                return false;
            if (x.Formulas.Count > 0 || x.Formulas.Count > 0)
                return false;
            if (x.GenericName != y.GenericName)
                return false;
            if (x.HasException != y.HasException)
                return false;
            if (x.InfoSupp != y.InfoSupp)
                return false;
            if (x.Strength != y.Strength)
                return false;

            return true;
        }

        public int GetHashCode(DrugV obj)
        {
            return obj.BrandName.GetHashCode() + obj.Form.GetHashCode() + obj.GenericName.GetHashCode() + obj.Strength.GetHashCode() + obj.UserFavourite.GetHashCode();
        }
    }

    public static class DBConverters
    {
        public static string GetNullableString(SqlDataReader dr, string fieldName)
        {
            var i = dr.GetOrdinal(fieldName);
            return !dr.IsDBNull(i) ? dr.GetString(i).TrimEnd() : string.Empty;
        }

        public static Guid GetGuid(SqlDataReader dr, string fieldName)
        {
            var i = dr.GetOrdinal(fieldName);
            return !dr.IsDBNull(i) ? dr.GetGuid(i) : Guid.Empty;
        }


        public static char GetNullableChar(SqlDataReader dr, string fieldName)
        {

            var i = dr.GetOrdinal(fieldName);
            if (!dr.IsDBNull(i))
            {
                char[] buff = new char[1];
                dr.GetChars(i, 0, buff, 0, 1);
                return buff[0];
            }
            else
            {
                return char.MaxValue;
            }
        }

        public static char[] GetNullableCharArray(SqlDataReader dr, string fieldName, int length)
        {

            var i = dr.GetOrdinal(fieldName);
            if (!dr.IsDBNull(i))
            {
                char[] buff = new char[length];
                dr.GetChars(i, 0, buff, 0, length);
                return buff;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Convert a Sql TinyInt to short
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static short GetNullableTinyIntToShortNumber(SqlDataReader dr, string fieldName)
        {
            try
            {
                var i = dr.GetOrdinal(fieldName);
                if (!dr.IsDBNull(i))
                    return (short)dr.GetByte(i);
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Short = smallint
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static short GetNullableShortNumber(SqlDataReader dr, string fieldName)
        {
            try
            {
                var i = dr.GetOrdinal(fieldName);
                if (!dr.IsDBNull(i))
                    return (short)dr.GetValue(i);
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Byte = tinyint
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static byte GetNullableByteNumber(SqlDataReader dr, string fieldName)
        {
            var i = dr.GetOrdinal(fieldName);
            if (!dr.IsDBNull(i))
                return (byte)dr.GetValue(i);
            else
                return 0;
        }
        public static byte GetNullableByte(SqlDataReader dr, string fieldName)
        {
            var i = dr.GetOrdinal(fieldName);
            if (!dr.IsDBNull(i))
                return dr.GetByte(i);
            else
                return 0;
        }
        public static DateTime? GetNullableDateTime(SqlDataReader dr, string fieldName)
        {
            var i = dr.GetOrdinal(fieldName);
            if (!dr.IsDBNull(i))
                return dr.GetDateTime(i);
            else
                return null;
        }

        public static DateTime GetDateTime(SqlDataReader dr, string fieldName)
        {
            var i = dr.GetOrdinal(fieldName);
            if (!dr.IsDBNull(i))
                return dr.GetDateTime(i);
            else
                return new DateTime();
        }

        /// <summary>
        /// Boolean = Bit
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static bool GetNullableBoolean(SqlDataReader dr, string fieldName)
        {
            var i = dr.GetOrdinal(fieldName);
            if (!dr.IsDBNull(i))
                return dr.GetBoolean(i);
            else
                return false;
        }

        /// <summary>
        /// Converts to Int16 (similar to a "smallInt" in SqlServer)
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static short GetNullableIntNumber(SqlDataReader dr, string fieldName)
        {
            var i = dr.GetOrdinal(fieldName);
            if (!dr.IsDBNull(i))
                return dr.GetInt16(i);
            else
                return 0;
        }

        /// <summary>
        /// Converts to Int32 (similar to a "int" in SqlServer)
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static int GetNullableInt32Number(SqlDataReader dr, string fieldName)
        {
            var i = dr.GetOrdinal(fieldName);
            if (!dr.IsDBNull(i))
                return dr.GetInt32(i);
            else
                return 0;
        }

        public static long GetNullableInt64Number(SqlDataReader dr, string fieldName)
        {
            var i = dr.GetOrdinal(fieldName);
            if (!dr.IsDBNull(i))
                return dr.GetInt64(i);
            else
                return 0;
        }

        public static float GetNullableSingleNumberOrDefault(SqlDataReader dr, string fieldName, float nullValue = 0)
        {
            var i = dr.GetOrdinal(fieldName);
            if (!dr.IsDBNull(i))
                return dr.GetFloat(i);
            else
                return nullValue;
        }

        public static float? GetNullableSingleNumber(SqlDataReader dr, string fieldName)
        {
            var i = dr.GetOrdinal(fieldName);
            if (!dr.IsDBNull(i))
                return dr.GetFloat(i);
            else
                return null;
        }

        public static double GetNullableDoubleNumberOrDefault(SqlDataReader dr, string fieldName, double nullValue = 0)
        {
            var i = dr.GetOrdinal(fieldName);
            if (!dr.IsDBNull(i))
                return dr.GetDouble(i);
            else
                return nullValue;
        }
        public static decimal GetNullableGetDecimalOrDefault(SqlDataReader dr, string fieldName, decimal nullValue = 0)
        {
            var i = dr.GetOrdinal(fieldName);
            if (!dr.IsDBNull(i))
                return dr.GetDecimal(i);
            else
                return nullValue;
        }
    }
}
