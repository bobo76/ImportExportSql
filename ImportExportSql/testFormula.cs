using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

namespace ImportExportSql
{
    class testFormula
    {
        private static List<ReplacementString> RouteList = new List<ReplacementString>();
        private static List<ReplacementString> FrequencyList = new List<ReplacementString>();
        private static ReplacementString PrnList = new ReplacementString("p.r.n.", new string[] { "p.r.n", "prn" });
        private static List<ReplacementString> DurationList = new List<ReplacementString>();
        private static List<ReplacementString> DoseList = new List<ReplacementString>();
        private static List<ReplacementString> QuantityList = new List<ReplacementString>();

        static testFormula()
        {
            //The mobile accept only a limited list of Route/Frequency/Duration.
            //Kin allow free text, so we need to replace 'accepted value' with the 'final value'
            RouteList.AddRange(new ReplacementString[] { new ReplacementString("p.o.", new string[]{ "p.o" }),
                new ReplacementString("o.d.", new string[]{ "o.d" }),
                new ReplacementString("o.s.", new string[]{ "o.s" }),
                new ReplacementString("o.u.", new string[]{ "o.u" }),
                new ReplacementString("p.o.", new string[]{ "p.o" })});
            var rteList = new string[] { "i.m intra-musculaire", "i.r.intra - rectal", "i.v intraveineux",
                "s.c.sous - cutané", "i.m.intra - muscular", "i.v.intravenous", "s.c.subcutaneous" };
            RouteList.AddRange(rteList.Select(t => new ReplacementString(t)).ToArray());

            FrequencyList.AddRange(new ReplacementString[] { new ReplacementString("die", new string[]{ "d.i.e." }),
                new ReplacementString("b.i.d.", new string[]{ "b.i.d..","b.i.d.","b.i.d","bid" }),
                new ReplacementString("t.i.d.", new string[]{ "t.i.d..","t.i.d.","t.i.d","tid" }),
                new ReplacementString("q.i.d.", new string[]{ "q.i.d..","q.i.d.","q.i.d","qid" }),
                new ReplacementString("h.s.", new string[]{ "h.s.", "h.s","hs" }),
                new ReplacementString("q 4 h", new string[]{ "q 4 heures","q 4h","q 4 h","q4h" }),
                new ReplacementString("q 4-6 h", new string[]{ "q 4 à 6 heures", "q 4-6h", "q 4-6 heures", "q 4-6 h", "q4-6h" }),
                new ReplacementString("q 6-8 h", new string[]{ "q 6 à 8 heures","q 6-8h","q 6-8 h","q6-8h" }),
                new ReplacementString("q 5 h", new string[]{ "q 5 heures","q 5h","q 5 h","q5h" }),
                new ReplacementString("q 6 h", new string[]{ "q 6 heures","q 6h","q 6 h","q6h" }),
                new ReplacementString("q 8 h", new string[]{ "q 8 heures","q 8h","q 8 h","q8h" }),
                new ReplacementString("q 12 h", new string[]{ "q 12 heures","q 12h","q 12 h","q12h" }),
                new ReplacementString("q 2 jrs", new string[]{ "q 2jrs", "q2jrs", "q2jr" }),
                new ReplacementString("q 3 jrs", new string[]{ "q 3jrs", "q3jrs", "q3jr" }),
                new ReplacementString("par semaine", new string[]{ "q1semaine", "q 1 semaine", "q 1 sem", "q1sem", "q semaine", "q sem", "1 fois / sem" })
            });
            var freqList = new string[]{ "a.c.","p.c.",
                "morning","noon","evening","supper",
                "q 2 days","q 3 days",
                "per week","per month","per day","per kg",
                "matin","midi","soir","souper",
                "par mois","par jour","par Kg" };
            FrequencyList.AddRange(freqList.Select(t => new ReplacementString(t)).ToArray());

            DurationList.AddRange(new ReplacementString[] { new ReplacementString("day(s)", new string[]{ "days", "day" }),
                new ReplacementString("week(s)", new string[]{ "weeks","week" }),
                new ReplacementString("jour(s)", new string[]{ "jours","jour","jrs" }),
                new ReplacementString("semaine(s)", new string[]{ "semaines","semaine","sem" }) });
            var DurList = new string[] { "month", "year", "mois", "an" };
            DurationList.AddRange(DurList.Select(t => new ReplacementString(t)).ToArray());

            var doseList = new string[] { "cm", "co l.a.", "dose(s)", "mg" };
            var doseListEn = new string[] { "blister(s)","apply","strip(s)","puff","bottle(s)","cartridge(s)","cc or ml","tablet(s)",
                "Tbl. spoon","Tea spoon","gel capsule(s)","drop(s)","ml or cc","plaster","packet(s)","suppository(ies)","patch(es)",
                "unit(s)","use","spray" };
            var doseListFr = new string[] { "ampoule(s)","appliquer","bandelette(s)","bouffée(s)","bouteille(s)","cartouche(s)","cc ou ml",
                "cuill. Soupe","cuill. Thé","gélule(s)","jet(s)","ml ou cc","pansement(s)","sachet(s)",
                "suppositoire(s)","unité(s)","utiliser" };
            DoseList.AddRange(doseList.Concat(doseListEn).Concat(doseListFr).Select(t => new ReplacementString(t)).ToArray());
            DoseList.AddRange(new ReplacementString[] { new ReplacementString("comprimé(s)", new string[] { "co.", "comp", "co" }),
                    new ReplacementString("capsule(s)", new string[] { "caps.", "caps", "cap" }),
                    new ReplacementString("timbre(s)", new string[] { "timbre" }),
                    new ReplacementString("application(s)", new string[] { "application", "appl.", "appl" }),
                    new ReplacementString("goutte(s)", new string[] { "gttes", "gtte" }),
                    new ReplacementString("inhalation(s)", new string[] { "inhalations.", "inhalations", "inhalation", "inh." }),
                    new ReplacementString("vaporisation(s)", new string[] { "vaporisations", "vaporisation", "vapo.", "vapo" })});

            var quantityList = new string[] { "cm", "dose(s)", "g", "kg", "mg", "ml", "qs" };
            var quantityListEn = new string[] { "comprimé(s)", "mètre(s)", "liter(s)" };
            var quantityListFr = new string[] { "tablet(s)", "meter(s)", "litre(s)" };
            QuantityList.AddRange(quantityList.Concat(quantityListEn).Concat(quantityListFr).Select(t => new ReplacementString(t)).ToArray());
        }

        public static void UpdateDrug(string connectionString)
        {
            var formulaList = GetDrug(connectionString);
            int tmp;
            float tmpf;
            string rst = null;
            foreach (var formula in formulaList.Where(t => t.Id == "4817"))
            {
                foreach (var replacement in RouteList)
                {
                    rst = replacement.TryReplace(formula.Administration, a => formula.Administration = a);
                    if (rst != null)
                    {
                        formula.RouteFinal = rst;
                        break;
                    }
                }

                foreach (var replacement in FrequencyList)
                {
                    rst = replacement.TryReplace(formula.Administration, a => formula.Administration = a);
                    if (rst != null)
                    {
                        formula.FrequencyFinal = rst;
                        break;
                    }
                }

                foreach (var replacement in DurationList)
                {
                    rst = replacement.TryReplace(formula.Duration, a => formula.Duration = a);
                    if (rst != null)
                    {
                        formula.DurationFinal = rst;
                        break;
                    }
                }
                formula.Duration = int.TryParse(formula.Duration, out tmp) ? "" : formula.Duration;
                if (tmp > 0)
                    formula.DurationFinal = (tmp + " " + formula.DurationFinal).Trim();

                foreach (var replacement in DoseList)
                {
                    rst = replacement.TryReplace(formula.Number, a => formula.Number = a);
                    if (rst != null)
                    {
                        formula.NumberFinal = rst;
                        break;
                    }
                }

                formula.Number = formula.Number.Replace("0.5", "0,5").Replace("1/2", "0,5");
                formula.Number = float.TryParse(formula.Number, out tmpf) ? "" : formula.Number;
                if (tmpf > 0)
                    formula.NumberFinal = (tmpf + " " + formula.NumberFinal).Trim();

                foreach (var replacement in QuantityList)
                {
                    rst = replacement.TryReplace(formula.Quantity, a => formula.Quantity = a);
                    if (rst != null)
                    {
                        formula.QuantityFinal = rst;
                        break;
                    }
                }
                formula.Quantity = int.TryParse(formula.Quantity, out tmp) ? "" : formula.Quantity;
                if (tmp > 0)
                    formula.QuantityFinal = (tmp + " " + formula.QuantityFinal).Trim();

                rst = PrnList.TryReplace(formula.Administration, a => formula.Administration = a);
                formula.Prn = (rst != null);

            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Id;valid;BrandName;AdministrationO;Administration;AdministrationF;DurationO;Duration;DurationF;NumberO;Number;NumberF;QuantityO;Quantity;QuantityF;Repetatur;CodeDIN;UserTemplate");

            foreach (var formula in formulaList)
            {
                sb.Append(formula.Id + ";");
                if (!string.IsNullOrWhiteSpace(formula.Administration) ||
                    !string.IsNullOrWhiteSpace(formula.Duration) ||
                    !string.IsNullOrWhiteSpace(formula.Quantity) ||
                    !string.IsNullOrWhiteSpace(formula.Number))
                    sb.Append("false;");
                else
                {
                    sb.Append("true;");
                    formula.IsValid = true;
                }

                sb.Append(formula.BrandName + ";");
                sb.Append(formula.AdministrationO + ";");
                sb.Append(formula.Administration + ";");
                sb.Append(formula.ToAdministration() + ";");
                sb.Append(formula.DurationO + ";");
                sb.Append(formula.Duration + ";");
                sb.Append(formula.Duration + " " + formula.DurationFinal + ";");
                sb.Append(formula.NumberO + ";");
                sb.Append(formula.Number + ";");
                sb.Append(formula.Number + " " + formula.NumberFinal + ";");
                sb.Append(formula.QuantityO + ";");
                sb.Append(formula.Quantity + ";");
                sb.Append(formula.Quantity + " " + formula.QuantityFinal + ";");
                sb.Append(formula.Repetatur + ";");
                sb.Append(formula.CodeDIN + ";");
                sb.AppendLine(formula.UserTemplate + ";");
            }
            System.IO.File.WriteAllText(@"c:\temp\Formula.csv", sb.ToString());
            SaveDrugs(formulaList.Where(t => t.Id == "4817").ToList(), connectionString);
        }


        public static List<DrugFormula> GetDrug(string connectionString)
        {
            var drugFormulaList = new List<DrugFormula>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("SELECT DrugFormula.id, DrugFormula.Number, DrugFormula.Administration, DrugFormula.Duration, DrugFormula.Quantity, DrugFormula.Repetatur, DrugFormula.CodeDIN, DrugFormula.Source, DrugV.BrandName " +
                        "FROM DrugFormula INNER JOIN DrugV ON DrugFormula.CodeDIN = DrugV.CodeDIN " +
                        "")
                {
                    Connection = con
                };
                con.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        drugFormulaList.Add(new DrugFormula(reader, ""));
                    }
                }
            }

            return drugFormulaList;
        }
        private static void SaveDrugs(List<DrugFormula> formulas, string connectionString)
        {
            var cmd = new SqlCommand("UPDATE DrugFormula SET Number = @Number, Administration = @Administration, Duration = @Duration, Quantity = @Quantity " +
                "WHERE (id = @id)");
            cmd.Parameters.Add(new SqlParameter("@Number", System.Data.SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@Administration", System.Data.SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@Duration", System.Data.SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@Quantity", System.Data.SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int));

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                cmd.Connection = con;
                foreach (var formula in formulas)
                {
                    formula.UpdateCommand(cmd);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
