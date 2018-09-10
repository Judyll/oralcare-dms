using System;
using System.Collections.Generic;
using System.Text;

namespace CommonExchange
{
    #region Common Structure Exchange

    [Serializable()]
    public struct Procedure
    {
        private String _procedureSystemId;
        public String ProcedureSystemId
        {
            get { return _procedureSystemId; }
            set { _procedureSystemId = value; }
        }

        private String _procedureName;
        public String ProcedureName
        {
            get { return _procedureName; }
            set { _procedureName = value; }
        }

        private Decimal _amount;
        public Decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
    }

    #endregion
}
