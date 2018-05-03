using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyDownloader
{
    class FormManager
    {
        private static FormManager m_Instance;
        public Dictionary<string, Form> FormMap;
        public static FormManager GetFormManager()
        {
            if(m_Instance == null)
            {
                m_Instance = new FormManager();
                m_Instance.FormMap = new Dictionary<string, Form>();
            }
            return m_Instance;
        }
    }
}
