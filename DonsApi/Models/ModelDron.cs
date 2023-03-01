using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonsApi.Models
{
    public class ModelDron
    {
        dronesEntities drons;
        
        public ModelDron()
        {
            this.drons = new dronesEntities();
        }


        //Get Drone with state this IDLE
        public List<dron> GetDronByState()
        {
            var consult = from dates in drons.dron
                           where dates.id_state == 1
                           select dates;
            if(consult.Count() == 0)
            {
                return null;
            }
            else
            {
                return consult.ToList();
            }
        }

        //Get Drone batery_capacity
        public string GetBateryDron(int iddron)
        {
            var consult = from dates in drons.dron
                           where dates.id == iddron
                           select dates.batery_capacity;
            if (consult.Count() == 0)
            {
                return null;
            }
            else
            {
                return Convert.ToString(consult + "%");
            }
        }

        //Get Batery down of 25%
        public bool Down25(int iddron)
        {
            var consult = from dates in drons.dron
                           where dates.batery_capacity < 25
                           select dates;
            if (consult.Count() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Get medication items loaded for a drone
        public List<string> GetMedicationByDron(int iddron)
        {
            List<drone_medication> list;
            List<string> list2 = null;
            var consult = from dates in drons.drone_medication
                           where dates.id == iddron
                           select dates;
            if (consult.Count() == 0)
            {
                list = null;
            }
            else
            {
                list = consult.ToList();
            }

            if(list != null)
            {
               
                foreach(var v in list)
                {
                    var consult2 = from dates in drons.medication
                                   where dates.id == v.id_medicamento
                                   select dates.name;
                    list2.Add(consult2.ToString());
                }
            }

            return list2;

        }


        //Get total weight of drone medications
        public double GetTotalWeightByDron(int iddron)
        {
            List<drone_medication> list;
            double weight = 0;
            var consult = from dates in drons.drone_medication
                           where dates.id == iddron
                           select dates;
            if (consult.Count() == 0)
            {
                list = null;
            }
            else
            {
                list = consult.ToList();
            }

            if (list != null)
            {

                foreach (var v in list)
                {
                    var consult2 = from dates in drons.medication
                                    where dates.id == v.id_medicamento
                                    select dates.weight;
                    weight = weight + Convert.ToDouble(consult2);
                }
            }

            return weight;

        }

        //Get weight to medication
        public double GetWeightByMedication(int idmedication)
        {
            
            
            var consult = from dates in drons.medication
                           where dates.id == idmedication
                           select dates.weight;
          
            return Convert.ToDouble(consult);

        }

        //Get weight to Dron
        public double GetWeightByDron(int iddron)
        {


            var consult = from dates in drons.dron
                           where dates.id == iddron
                           select dates.weight_limit;

            return Convert.ToDouble(consult);

        }






    }
}