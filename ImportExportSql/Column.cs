﻿namespace ImportExportSql
{
    public class Column
    {
        public string Name { get; set; }
        public bool IsNullable { get; set; }
        public IDataType Type { get; set; }
        public int DataIndex { get; set; }

        public override string ToString()
        {
            return $"{Name} (" + (IsNullable ? "Nullable" : "Not nullable") + "))";
        }
    }
}
