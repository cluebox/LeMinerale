using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SpssLib.DataReader;
using SpssLib.SpssDataset;
using System.Data.SqlClient;

namespace LeMineraleParsing
{
    class LeMinerale
    {
        static void Main(string[] args)
        {
            int SurveyID = 600591;
            string SURVEY_PERIOD = "2018-08-31";//survey period
            string country = "INDONESIA";//survey country
            InsertionLeMinerale iobj = new InsertionLeMinerale();
            string questions = "iobs,weight,r4,r131,r191,r6,r130,r132,r200,qwave,r21a,r21b_12,r21b_6,r21b_2,r21b_18,r21b_9,r21b_25,r21b_19,r21b_20,r21b_7,r21e_12,r21e_6,r21e_2,r21e_18,r21e_9,r21e_25,r21e_19,r21e_20,r21e_7,r21c,r21d_12,r21d_6,r21d_2,r21d_18,r21d_9,r21d_25,r21d_19,r21d_20,r21d_7,r21f_12,r21f_6,r21f_2,r21f_18,r21f_9,r21f_25,r21f_19,r21f_20,r21f_7,r22a_12,r22a_6,r22a_2,r22a_18,r22a_9,r22a_25,r22a_19,r22a_20,r22a_7,r22b_12,r22b_6,r22b_2,r22b_18,r22b_9,r22b_25,r22b_19,r22b_20,r22b_7,r23a_12,r23a_6,r23a_2,r23a_18,r23a_9,r23a_25,r23a_19,r23a_20,r23a_7,r23b_12,r23b_6,r23b_2,r23b_18,r23b_9,r23b_25,r23b_19,r23b_20,r23b_7,r22cr12,r22cr6,r22cr2,r22cr18,r22cr9,r22cr25,r22cr19,r22cr20,r22cr7,r23c,r28,r26a,r26b,r27a_12,r27a_6,r27a_2,r27a_18,r27a_9,r27a_25,r27a_19,r27a_20,r27a_7,r26c,q193at5_7,q193at5_17,q193at5_18,q193at5_24,q193at5_26,q193at5_27,q193at3_7,q193at3_17,q193at3_18,q193at3_24,q193at3_26,q193at3_27,q193at2_7,q193at2_17,q193at2_18,q193at2_24,q193at2_26,q193at2_27,q193at6_7,q193at6_17,q193at6_18,q193at6_24,q193at6_26,q193at6_27,q193at4_7,q193at4_17,q193at4_18,q193at4_24,q193at4_26,q193at4_27,q193at9_7,q193at9_17,q193at9_18,q193at9_24,q193at9_26,q193at9_27,q193at7_7,q193at7_17,q193at7_18,q193at7_24,q193at7_26,q193at7_27,q193at8_7,q193at8_17,q193at8_18,q193at8_24,q193at8_26,q193at8_27,q193at1_7,q193at1_17,q193at1_18,q193at1_24,q193at1_26,q193at1_27,r15a_6,r15a_7,r15a_8,r15a_9,r15a_10,r15a_11,r15a_12,r15a_13,r15a_14,r15a_15,r15a_16,r15a_17,r15a_18,r15b_6,r15b_7,r15b_8,r15b_9,r15b_10,r15b_11,r15b_12,r15b_13,r15b_14,r15b_15,r15b_16,r15b_17,r15b_18,r15c_6,r15c_7,r15c_8,r15c_9,r15c_10,r15c_11,r15c_12,r15c_13,r15c_14,r15c_15,r15c_16,r15c_17,r15c_18,r15d_6,r15d_7,r15d_8,r15d_9,r15d_10,r15d_11,r15d_12,r15d_13,r15d_14,r15d_15,r15d_16,r15d_17,r15d_18,r18,r16a_1,r16a_2,r16a_3,r16a_4,r16a_5,r16a_6,r16a_7,r16b_1,r16b_2,r16b_3,r16b_4,r16b_5,r16b_6,r16b_7,r16c_1,r16c_2,r16c_3,r16c_4,r16c_5,r16c_6,r16c_7,r16d,q193at11_7,q193at11_17,q193at11_18,q193at11_24,q193at11_26,q193at11_27,q193at12_7,q193at12_17,q193at12_18,q193at12_24,q193at12_26,q193at12_27,q193at13_7,q193at13_17,q193at13_18,q193at13_24,q193at13_26,q193at13_27,r23d_12,r23d_6,r23d_2,r23d_18,r23d_9,r23d_25,r23d_19,r23d_20,r23d_7";// dashboard variable value
            //string questions = "r23d_12,r23d_6,r23d_2,r23d_18,r23d_9,r23d_25,r23d_19,r23d_20,r23d_7";
            string[] spss_variable_name = questions.Split(',');
            SpssReader spssDataset;
            using (FileStream fileStream = new FileStream(@"E:\E\SPSS\Aug 2018\Aug_  lemineral\Lemineral_Aug2018.sav", FileMode.Open, FileAccess.Read, FileShare.Read, 2048 * 10, FileOptions.SequentialScan))
            {
                spssDataset = new SpssReader(fileStream); // Create the reader, this will read the file header
                foreach (string spss_v in spss_variable_name)
                {
                    foreach (var variable in spssDataset.Variables)  // Iterate through all the varaibles
                    {
                        if (variable.Name.Equals(spss_v))
                        {
                            foreach (KeyValuePair<double, string> label in variable.ValueLabels)
                            {
                                string VARIABLE_NAME = spss_v;
                                string VARIABLE_NAME_SUB_NAME = variable.Name;
                                string VARIABLE_ID = label.Key.ToString();
                                string VARIABLE_VALUE = label.Value;
                                string VARIABLE_NAME_QUESTION = variable.Label;
                                string BASE_VARIABLE_NAME = variable.Name;
                                //iobj.insert_Dashboard_variable_values(VARIABLE_NAME, VARIABLE_NAME_SUB_NAME, VARIABLE_ID, VARIABLE_VALUE, VARIABLE_NAME_QUESTION, SurveyID, country, BASE_VARIABLE_NAME, SURVEY_PERIOD);
                            }
                        }
                    }
                }
                // Iterate through all data rows in the file
                foreach (var record in spssDataset.Records)
                {
                    string userID = null;
                    string variable_name;
                    decimal Weight = 0;
                    string u_id = "-- Not Available --";
                    string Gender = "-- Not Available --";
                    string MaritalStatus = "-- Not Available --";
                    string AttendedOn = "-- Not Available --";
                    string Location = "-- Not Available --";
                    string AgeGroup = "-- Not Available --";
                    string Education = "-- Not Available --";
                    string PersonalInc = "-- Not Available --";
                    string SEC = "-- Not Available --";
                    string Period = "-- Not Available --";
                    string BrTom = "-- Not Available --";
                    string BrSpont_LeMinerale = "-- Not Available --";
                    string BrSpont_Aqua = "-- Not Available --";
                    string BrSpont_Ades = "-- Not Available --";
                    string BrSpont_NestlePureLife = "-- Not Available --";
                    string BrSpont_Club = "-- Not Available --";
                    string BrSpont_VIT = "-- Not Available --";
                    string BrSpont_Oasis = "-- Not Available --";
                    string BrSpont_PrimA = "-- Not Available --";
                    string BrSpont_Axo = "-- Not Available --";
                    string BrAid_LeMinerale = "-- Not Available --";
                    string BrAid_Aqua = "-- Not Available --";
                    string BrAid_Ades = "-- Not Available --";
                    string BrAid_NestlePureLife = "-- Not Available --";
                    string BrAid_Club = "-- Not Available --";
                    string BrAid_VIT = "-- Not Available --";
                    string BrAid_Oasis = "-- Not Available --";
                    string BrAid_PrimA = "-- Not Available --";
                    string BrAid_Axo = "-- Not Available --";
                    string AdTom = "-- Not Available --";
                    string AdSpont_LeMinerale = "-- Not Available --";
                    string AdSpont_Aqua = "-- Not Available --";
                    string AdSpont_Ades = "-- Not Available --";
                    string AdSpont_NestlePureLife = "-- Not Available --";
                    string AdSpont_Club = "-- Not Available --";
                    string AdSpont_VIT = "-- Not Available --";
                    string AdSpont_Oasis = "-- Not Available --";
                    string AdSpont_PrimA = "-- Not Available --";
                    string AdSpont_Axo = "-- Not Available --";
                    string AdAid_LeMinerale = "-- Not Available --";
                    string AdAid_Aqua = "-- Not Available --";
                    string AdAid_Ades = "-- Not Available --";
                    string AdAid_NestlePureLife = "-- Not Available --";
                    string AdAid_Club = "-- Not Available --";
                    string AdAid_VIT = "-- Not Available --";
                    string AdAid_Oasis = "-- Not Available --";
                    string AdAid_PrimA = "-- Not Available --";
                    string AdAid_Axo = "-- Not Available --";
                    string EverCons_LeMinerale = "-- Not Available --";
                    string EverCons_Aqua = "-- Not Available --";
                    string EverCons_Ades = "-- Not Available --";
                    string EverCons_NestlePureLife = "-- Not Available --";
                    string EverCons_Club = "-- Not Available --";
                    string EverCons_VIT = "-- Not Available --";
                    string EverCons_Oasis = "-- Not Available --";
                    string EverCons_PrimA = "-- Not Available --";
                    string EverCons_Axo = "-- Not Available --";
                    string ConsP3M_LeMinerale = "-- Not Available --";
                    string ConsP3M_Aqua = "-- Not Available --";
                    string ConsP3M_Ades = "-- Not Available --";
                    string ConsP3M_NestlePureLife = "-- Not Available --";
                    string ConsP3M_Club = "-- Not Available --";
                    string ConsP3M_VIT = "-- Not Available --";
                    string ConsP3M_Oasis = "-- Not Available --";
                    string ConsP3M_PrimA = "-- Not Available --";
                    string ConsP3M_Axo = "-- Not Available --";
                    string ConsP1M_LeMinerale = "-- Not Available --";
                    string ConsP1M_Aqua = "-- Not Available --";
                    string ConsP1M_Ades = "-- Not Available --";
                    string ConsP1M_NestlePureLife = "-- Not Available --";
                    string ConsP1M_Club = "-- Not Available --";
                    string ConsP1M_VIT = "-- Not Available --";
                    string ConsP1M_Oasis = "-- Not Available --";
                    string ConsP1M_PrimA = "-- Not Available --";
                    string ConsP1M_Axo = "-- Not Available --";
                    string ConsP1W_LeMinerale = "-- Not Available --";
                    string ConsP1W_Aqua = "-- Not Available --";
                    string ConsP1W_Ades = "-- Not Available --";
                    string ConsP1W_NestlePureLife = "-- Not Available --";
                    string ConsP1W_Club = "-- Not Available --";
                    string ConsP1W_VIT = "-- Not Available --";
                    string ConsP1W_Oasis = "-- Not Available --";
                    string ConsP1W_PrimA = "-- Not Available --";
                    string ConsP1W_Axo = "-- Not Available --";
                    string frConsP3M_LeMinerale = "-- Not Available --";
                    string frConsP3M_Aqua = "-- Not Available --";
                    string frConsP3M_Ades = "-- Not Available --";
                    string frConsP3M_NestlePureLife = "-- Not Available --";
                    string frConsP3M_Club = "-- Not Available --";
                    string frConsP3M_VIT = "-- Not Available --";
                    string frConsP3M_Oasis = "-- Not Available --";
                    string frConsP3M_PrimA = "-- Not Available --";
                    string frConsP3M_Axo = "-- Not Available --";
                    string Bumo = "-- Not Available --";
                    string PreBumo = "-- Not Available --";
                    string FavBrand = "-- Not Available --";
                    string SecFavBrand = "-- Not Available --";
                    string ConToBuy_LeMinerale = "-- Not Available --";
                    string ConToBuy_Aqua = "-- Not Available --";
                    string ConToBuy_Ades = "-- Not Available --";
                    string ConToBuy_NestlePureLife = "-- Not Available --";
                    string ConToBuy_Club = "-- Not Available --";
                    string ConToBuy_VIT = "-- Not Available --";
                    string ConToBuy_Oasis = "-- Not Available --";
                    string ConToBuy_PrimA = "-- Not Available --";
                    string ConToBuy_Axo = "-- Not Available --";
                    string Recommendation = "-- Not Available --";
                    string q193at5_7 = "-- Not Available --";
                    string q193at5_17 = "-- Not Available --";
                    string q193at5_18 = "-- Not Available --";
                    string q193at5_24 = "-- Not Available --";
                    string q193at5_26 = "-- Not Available --";
                    string q193at5_27 = "-- Not Available --";
                    string q193at3_7 = "-- Not Available --";
                    string q193at3_17 = "-- Not Available --";
                    string q193at3_18 = "-- Not Available --";
                    string q193at3_24 = "-- Not Available --";
                    string q193at3_26 = "-- Not Available --";
                    string q193at3_27 = "-- Not Available --";
                    string q193at2_7 = "-- Not Available --";
                    string q193at2_17 = "-- Not Available --";
                    string q193at2_18 = "-- Not Available --";
                    string q193at2_24 = "-- Not Available --";
                    string q193at2_26 = "-- Not Available --";
                    string q193at2_27 = "-- Not Available --";
                    string q193at6_7 = "-- Not Available --";
                    string q193at6_17 = "-- Not Available --";
                    string q193at6_18 = "-- Not Available --";
                    string q193at6_24 = "-- Not Available --";
                    string q193at6_26 = "-- Not Available --";
                    string q193at6_27 = "-- Not Available --";
                    string q193at4_7 = "-- Not Available --";
                    string q193at4_17 = "-- Not Available --";
                    string q193at4_18 = "-- Not Available --";
                    string q193at4_24 = "-- Not Available --";
                    string q193at4_26 = "-- Not Available --";
                    string q193at4_27 = "-- Not Available --";
                    string q193at9_7 = "-- Not Available --";
                    string q193at9_17 = "-- Not Available --";
                    string q193at9_18 = "-- Not Available --";
                    string q193at9_24 = "-- Not Available --";
                    string q193at9_26 = "-- Not Available --";
                    string q193at9_27 = "-- Not Available --";
                    string q193at7_7 = "-- Not Available --";
                    string q193at7_17 = "-- Not Available --";
                    string q193at7_18 = "-- Not Available --";
                    string q193at7_24 = "-- Not Available --";
                    string q193at7_26 = "-- Not Available --";
                    string q193at7_27 = "-- Not Available --";
                    string q193at8_7 = "-- Not Available --";
                    string q193at8_17 = "-- Not Available --";
                    string q193at8_18 = "-- Not Available --";
                    string q193at8_24 = "-- Not Available --";
                    string q193at8_26 = "-- Not Available --";
                    string q193at8_27 = "-- Not Available --";
                    string q193at1_7 = "-- Not Available --";
                    string q193at1_17 = "-- Not Available --";
                    string q193at1_18 = "-- Not Available --";
                    string q193at1_24 = "-- Not Available --";
                    string q193at1_26 = "-- Not Available --";
                    string q193at1_27 = "-- Not Available --";
                    string P6M_BottledWater = "-- Not Available --";
                    string P6M_SoftDrink = "-- Not Available --";
                    string P6M_Juice_RTDjuice = "-- Not Available --";
                    string P6M_RTD_tea = "-- Not Available --";
                    string P6M_RTD_milk = "-- Not Available --";
                    string P6M_EnergyDrinks = "-- Not Available --";
                    string P6M_IsotonicDrinks = "-- Not Available --";
                    string P6M_RTD_pkd_coffee = "-- Not Available --";
                    string P6M_VitaminDrinks = "-- Not Available --";
                    string P6M_FermentedMilk_RTDyoghurt = "-- Not Available --";
                    string P6M_TeaBag = "-- Not Available --";
                    string P6M_InstantCoffee = "-- Not Available --";
                    string P6M_MilkPowder = "-- Not Available --";
                    string P3M_BottledWater = "-- Not Available --";
                    string P3M_SoftDrink = "-- Not Available --";
                    string P3M_Juice_RTDjuice = "-- Not Available --";
                    string P3M_RTD_tea = "-- Not Available --";
                    string P3M_RTD_milk = "-- Not Available --";
                    string P3M_EnergyDrinks = "-- Not Available --";
                    string P3M_IsotonicDrinks = "-- Not Available --";
                    string P3M_RTD_pkd_coffee = "-- Not Available --";
                    string P3M_VitaminDrinks = "-- Not Available --";
                    string P3M_FermentedMilk_RTDyoghurt = "-- Not Available --";
                    string P3M_TeaBag = "-- Not Available --";
                    string P3M_InstantCoffee = "-- Not Available --";
                    string P3M_MilkPowder = "-- Not Available --";
                    string P1M_BottledWater = "-- Not Available --";
                    string P1M_SoftDrink = "-- Not Available --";
                    string P1M_Juice_RTDjuice = "-- Not Available --";
                    string P1M_RTD_tea = "-- Not Available --";
                    string P1M_RTD_milk = "-- Not Available --";
                    string P1M_EnergyDrinks = "-- Not Available --";
                    string P1M_IsotonicDrinks = "-- Not Available --";
                    string P1M_RTD_pkd_coffee = "-- Not Available --";
                    string P1M_VitaminDrinks = "-- Not Available --";
                    string P1M_FermentedMilk_RTDyoghurt = "-- Not Available --";
                    string P1M_TeaBag = "-- Not Available --";
                    string P1M_InstantCoffee = "-- Not Available --";
                    string P1M_MilkPowder = "-- Not Available --";
                    string P1W_BottledWater = "-- Not Available --";
                    string P1W_SoftDrink = "-- Not Available --";
                    string P1W_Juice_RTDjuice = "-- Not Available --";
                    string P1W_RTD_tea = "-- Not Available --";
                    string P1W_RTD_milk = "-- Not Available --";
                    string P1W_EnergyDrinks = "-- Not Available --";
                    string P1W_IsotonicDrinks = "-- Not Available --";
                    string P1W_RTD_pkd_coffee = "-- Not Available --";
                    string P1W_VitaminDrinks = "-- Not Available --";
                    string P1W_FermentedMilk_RTDyoghurt = "-- Not Available --";
                    string P1W_TeaBag = "-- Not Available --";
                    string P1W_InstantCoffee = "-- Not Available --";
                    string P1W_MilkPowder = "-- Not Available --";
                    string r18 = "-- Not Available --";
                    string P3M_Plastic_cup = "-- Not Available --";
                    string P3M_S_PlasticBottle_330ml = "-- Not Available --";
                    string P3M_M_PlasticBottle_600ml = "-- Not Available --";
                    string P3M_L_PlasticBottle_1500ml = "-- Not Available --";
                    string P3M_GlassBottle = "-- Not Available --";
                    string P3M_Gallon = "-- Not Available --";
                    string P3M_PlasticBottle_750ml = "-- Not Available --";
                    string P1M_Plastic_cup = "-- Not Available --";
                    string P1M_S_PlasticBottle_330ml = "-- Not Available --";
                    string P1M_M_PlasticBottle_600ml = "-- Not Available --";
                    string P1M_L_PlasticBottle_1500ml = "-- Not Available --";
                    string P1M_GlassBottle = "-- Not Available --";
                    string P1M_Gallon = "-- Not Available --";
                    string P1M_PlasticBottle_750ml = "-- Not Available --";
                    string P1W_Plastic_cup = "-- Not Available --";
                    string P1W_S_PlasticBottle_330ml = "-- Not Available --";
                    string P1W_M_PlasticBottle_600ml = "-- Not Available --";
                    string P1W_L_PlasticBottle_1500ml = "-- Not Available --";
                    string P1W_GlassBottle = "-- Not Available --";
                    string P1W_Gallon = "-- Not Available --";
                    string P1W_PlasticBottle_750ml = "-- Not Available --";
                    string SKU_BUMO = "-- Not Available --";
                    string q193at11_7 = "-- Not Available --";
                    string q193at11_17 = "-- Not Available --";
                    string q193at11_18 = "-- Not Available --";
                    string q193at11_24 = "-- Not Available --";
                    string q193at11_26 = "-- Not Available --";
                    string q193at11_27 = "-- Not Available --";
                    string q193at12_7 = "-- Not Available --";
                    string q193at12_17 = "-- Not Available --";
                    string q193at12_18 = "-- Not Available --";
                    string q193at12_24 = "-- Not Available --";
                    string q193at12_26 = "-- Not Available --";
                    string q193at12_27 = "-- Not Available --";
                    string q193at13_7 = "-- Not Available --";
                    string q193at13_17 = "-- Not Available --";
                    string q193at13_18 = "-- Not Available --";
                    string q193at13_24 = "-- Not Available --";
                    string q193at13_26 = "-- Not Available --";
                    string q193at13_27 = "-- Not Available --";
                    string ConsP1D_LeMinerale = "-- Not Available --";
                    string ConsP1D_Aqua = "-- Not Available --";
                    string ConsP1D_Ades = "-- Not Available --";
                    string ConsP1D_NestlePureLife = "-- Not Available --";
                    string ConsP1D_Club = "-- Not Available --";
                    string ConsP1D_VIT = "-- Not Available --";
                    string ConsP1D_Oasis = "-- Not Available --";
                    string ConsP1D_PrimA = "-- Not Available --";
                    string ConsP1D_Axo = "-- Not Available --";


                    foreach (var variable in spssDataset.Variables)
                    {
                        foreach (string spss_v in spss_variable_name)
                        {
                            if (variable.Name.Equals(spss_v))
                            {
                                variable_name = variable.Name;
                                switch (variable_name)
                                {
                                    case "iobs":
                                        {
                                            u_id = Convert.ToString(record.GetValue(variable));
                                            userID = find_UserId(SurveyID, SURVEY_PERIOD, u_id);
                                            //userID = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "weight":
                                        {
                                            Weight = Convert.ToDecimal(record.GetValue(variable));
                                            break;
                                        }
                                    case "r4":
                                        {
                                            Gender = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r131":
                                        {
                                            MaritalStatus = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r191":
                                        {
                                            Location = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r6":
                                        {
                                            AgeGroup = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r130":
                                        {
                                            Education = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r132":
                                        {
                                            PersonalInc = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r200":
                                        {
                                            SEC = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "qwave":
                                        {
                                            Period = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21a":
                                        {
                                            BrTom = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21b_12":
                                        {
                                            BrSpont_LeMinerale = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21b_6":
                                        {
                                            BrSpont_Aqua = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21b_2":
                                        {
                                            BrSpont_Ades = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21b_18":
                                        {
                                            BrSpont_NestlePureLife = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21b_9":
                                        {
                                            BrSpont_Club = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21b_25":
                                        {
                                            BrSpont_VIT = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21b_19":
                                        {
                                            BrSpont_Oasis = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21b_20":
                                        {
                                            BrSpont_PrimA = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21b_7":
                                        {
                                            BrSpont_Axo = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21e_12":
                                        {
                                            BrAid_LeMinerale = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21e_6":
                                        {
                                            BrAid_Aqua = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21e_2":
                                        {
                                            BrAid_Ades = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21e_18":
                                        {
                                            BrAid_NestlePureLife = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21e_9":
                                        {
                                            BrAid_Club = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21e_25":
                                        {
                                            BrAid_VIT = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21e_19":
                                        {
                                            BrAid_Oasis = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21e_20":
                                        {
                                            BrAid_PrimA = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21e_7":
                                        {
                                            BrAid_Axo = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21c":
                                        {
                                            AdTom = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21d_12":
                                        {
                                            AdSpont_LeMinerale = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21d_6":
                                        {
                                            AdSpont_Aqua = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21d_2":
                                        {
                                            AdSpont_Ades = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21d_18":
                                        {
                                            AdSpont_NestlePureLife = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21d_9":
                                        {
                                            AdSpont_Club = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21d_25":
                                        {
                                            AdSpont_VIT = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21d_19":
                                        {
                                            AdSpont_Oasis = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21d_20":
                                        {
                                            AdSpont_PrimA = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21d_7":
                                        {
                                            AdSpont_Axo = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21f_12":
                                        {
                                            AdAid_LeMinerale = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21f_6":
                                        {
                                            AdAid_Aqua = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21f_2":
                                        {
                                            AdAid_Ades = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21f_18":
                                        {
                                            AdAid_NestlePureLife = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21f_9":
                                        {
                                            AdAid_Club = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21f_25":
                                        {
                                            AdAid_VIT = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21f_19":
                                        {
                                            AdAid_Oasis = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21f_20":
                                        {
                                            AdAid_PrimA = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r21f_7":
                                        {
                                            AdAid_Axo = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22a_12":
                                        {
                                            EverCons_LeMinerale = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22a_6":
                                        {
                                            EverCons_Aqua = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22a_2":
                                        {
                                            EverCons_Ades = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22a_18":
                                        {
                                            EverCons_NestlePureLife = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22a_9":
                                        {
                                            EverCons_Club = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22a_25":
                                        {
                                            EverCons_VIT = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22a_19":
                                        {
                                            EverCons_Oasis = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22a_20":
                                        {
                                            EverCons_PrimA = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22a_7":
                                        {
                                            EverCons_Axo = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22b_12":
                                        {
                                            ConsP3M_LeMinerale = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22b_6":
                                        {
                                            ConsP3M_Aqua = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22b_2":
                                        {
                                            ConsP3M_Ades = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22b_18":
                                        {
                                            ConsP3M_NestlePureLife = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22b_9":
                                        {
                                            ConsP3M_Club = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22b_25":
                                        {
                                            ConsP3M_VIT = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22b_19":
                                        {
                                            ConsP3M_Oasis = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22b_20":
                                        {
                                            ConsP3M_PrimA = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22b_7":
                                        {
                                            ConsP3M_Axo = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23a_12":
                                        {
                                            ConsP1M_LeMinerale = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23a_6":
                                        {
                                            ConsP1M_Aqua = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23a_2":
                                        {
                                            ConsP1M_Ades = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23a_18":
                                        {
                                            ConsP1M_NestlePureLife = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23a_9":
                                        {
                                            ConsP1M_Club = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23a_25":
                                        {
                                            ConsP1M_VIT = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23a_19":
                                        {
                                            ConsP1M_Oasis = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23a_20":
                                        {
                                            ConsP1M_PrimA = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23a_7":
                                        {
                                            ConsP1M_Axo = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23b_12":
                                        {
                                            ConsP1W_LeMinerale = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23b_6":
                                        {
                                            ConsP1W_Aqua = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23b_2":
                                        {
                                            ConsP1W_Ades = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23b_18":
                                        {
                                            ConsP1W_NestlePureLife = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23b_9":
                                        {
                                            ConsP1W_Club = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23b_25":
                                        {
                                            ConsP1W_VIT = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23b_19":
                                        {
                                            ConsP1W_Oasis = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23b_20":
                                        {
                                            ConsP1W_PrimA = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23b_7":
                                        {
                                            ConsP1W_Axo = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22cr12":
                                        {
                                            frConsP3M_LeMinerale = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22cr6":
                                        {
                                            frConsP3M_Aqua = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22cr2":
                                        {
                                            frConsP3M_Ades = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22cr18":
                                        {
                                            frConsP3M_NestlePureLife = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22cr9":
                                        {
                                            frConsP3M_Club = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22cr25":
                                        {
                                            frConsP3M_VIT = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22cr19":
                                        {
                                            frConsP3M_Oasis = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22cr20":
                                        {
                                            frConsP3M_PrimA = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r22cr7":
                                        {
                                            frConsP3M_Axo = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23c":
                                        {
                                            Bumo = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r28":
                                        {
                                            PreBumo = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r26a":
                                        {
                                            FavBrand = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r26b":
                                        {
                                            SecFavBrand = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r27a_12":
                                        {
                                            ConToBuy_LeMinerale = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r27a_6":
                                        {
                                            ConToBuy_Aqua = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r27a_2":
                                        {
                                            ConToBuy_Ades = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r27a_18":
                                        {
                                            ConToBuy_NestlePureLife = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r27a_9":
                                        {
                                            ConToBuy_Club = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r27a_25":
                                        {
                                            ConToBuy_VIT = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r27a_19":
                                        {
                                            ConToBuy_Oasis = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r27a_20":
                                        {
                                            ConToBuy_PrimA = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r27a_7":
                                        {
                                            ConToBuy_Axo = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r26c":
                                        {
                                            Recommendation = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at5_7":
                                        {
                                            q193at5_7 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at5_17":
                                        {
                                            q193at5_17 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at5_18":
                                        {
                                            q193at5_18 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at5_24":
                                        {
                                            q193at5_24 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at5_26":
                                        {
                                            q193at5_26 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at5_27":
                                        {
                                            q193at5_27 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at3_7":
                                        {
                                            q193at3_7 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at3_17":
                                        {
                                            q193at3_17 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at3_18":
                                        {
                                            q193at3_18 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at3_24":
                                        {
                                            q193at3_24 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at3_26":
                                        {
                                            q193at3_26 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at3_27":
                                        {
                                            q193at3_27 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at2_7":
                                        {
                                            q193at2_7 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at2_17":
                                        {
                                            q193at2_17 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at2_18":
                                        {
                                            q193at2_18 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at2_24":
                                        {
                                            q193at2_24 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at2_26":
                                        {
                                            q193at2_26 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at2_27":
                                        {
                                            q193at2_27 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at6_7":
                                        {
                                            q193at6_7 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at6_17":
                                        {
                                            q193at6_17 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at6_18":
                                        {
                                            q193at6_18 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at6_24":
                                        {
                                            q193at6_24 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at6_26":
                                        {
                                            q193at6_26 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at6_27":
                                        {
                                            q193at6_27 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at4_7":
                                        {
                                            q193at4_7 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at4_17":
                                        {
                                            q193at4_17 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at4_18":
                                        {
                                            q193at4_18 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at4_24":
                                        {
                                            q193at4_24 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at4_26":
                                        {
                                            q193at4_26 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at4_27":
                                        {
                                            q193at4_27 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at9_7":
                                        {
                                            q193at9_7 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at9_17":
                                        {
                                            q193at9_17 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at9_18":
                                        {
                                            q193at9_18 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at9_24":
                                        {
                                            q193at9_24 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at9_26":
                                        {
                                            q193at9_26 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at9_27":
                                        {
                                            q193at9_27 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at7_7":
                                        {
                                            q193at7_7 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at7_17":
                                        {
                                            q193at7_17 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at7_18":
                                        {
                                            q193at7_18 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at7_24":
                                        {
                                            q193at7_24 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at7_26":
                                        {
                                            q193at7_26 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at7_27":
                                        {
                                            q193at7_27 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at8_7":
                                        {
                                            q193at8_7 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at8_17":
                                        {
                                            q193at8_17 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at8_18":
                                        {
                                            q193at8_18 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at8_24":
                                        {
                                            q193at8_24 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at8_26":
                                        {
                                            q193at8_26 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at8_27":
                                        {
                                            q193at8_27 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at1_7":
                                        {
                                            q193at1_7 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at1_17":
                                        {
                                            q193at1_17 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at1_18":
                                        {
                                            q193at1_18 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at1_24":
                                        {
                                            q193at1_24 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at1_26":
                                        {
                                            q193at1_26 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at1_27":
                                        {
                                            q193at1_27 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r15a_6":
                                        {
                                            P6M_BottledWater = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15a_7":
                                        {
                                            P6M_SoftDrink = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15a_8":
                                        {
                                            P6M_Juice_RTDjuice = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15a_9":
                                        {
                                            P6M_RTD_tea = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15a_10":
                                        {
                                            P6M_RTD_milk = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15a_11":
                                        {
                                            P6M_EnergyDrinks = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15a_12":
                                        {
                                            P6M_IsotonicDrinks = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15a_13":
                                        {
                                            P6M_RTD_pkd_coffee = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15a_14":
                                        {
                                            P6M_VitaminDrinks = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15a_15":
                                        {
                                            P6M_FermentedMilk_RTDyoghurt = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15a_16":
                                        {
                                            P6M_TeaBag = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15a_17":
                                        {
                                            P6M_InstantCoffee = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15a_18":
                                        {
                                            P6M_MilkPowder = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15b_6":
                                        {
                                            P3M_BottledWater = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15b_7":
                                        {
                                            P3M_SoftDrink = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15b_8":
                                        {
                                            P3M_Juice_RTDjuice = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15b_9":
                                        {
                                            P3M_RTD_tea = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15b_10":
                                        {
                                            P3M_RTD_milk = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15b_11":
                                        {
                                            P3M_EnergyDrinks = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15b_12":
                                        {
                                            P3M_IsotonicDrinks = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15b_13":
                                        {
                                            P3M_RTD_pkd_coffee = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15b_14":
                                        {
                                            P3M_VitaminDrinks = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15b_15":
                                        {
                                            P3M_FermentedMilk_RTDyoghurt = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15b_16":
                                        {
                                            P3M_TeaBag = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15b_17":
                                        {
                                            P3M_InstantCoffee = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15b_18":
                                        {
                                            P3M_MilkPowder = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15c_6":
                                        {
                                            P1M_BottledWater = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15c_7":
                                        {
                                            P1M_SoftDrink = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15c_8":
                                        {
                                            P1M_Juice_RTDjuice = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15c_9":
                                        {
                                            P1M_RTD_tea = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15c_10":
                                        {
                                            P1M_RTD_milk = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15c_11":
                                        {
                                            P1M_EnergyDrinks = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15c_12":
                                        {
                                            P1M_IsotonicDrinks = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15c_13":
                                        {
                                            P1M_RTD_pkd_coffee = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15c_14":
                                        {
                                            P1M_VitaminDrinks = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15c_15":
                                        {
                                            P1M_FermentedMilk_RTDyoghurt = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15c_16":
                                        {
                                            P1M_TeaBag = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15c_17":
                                        {
                                            P1M_InstantCoffee = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15c_18":
                                        {
                                            P1M_MilkPowder = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15d_6":
                                        {
                                            P1W_BottledWater = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15d_7":
                                        {
                                            P1W_SoftDrink = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15d_8":
                                        {
                                            P1W_Juice_RTDjuice = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15d_9":
                                        {
                                            P1W_RTD_tea = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15d_10":
                                        {
                                            P1W_RTD_milk = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15d_11":
                                        {
                                            P1W_EnergyDrinks = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15d_12":
                                        {
                                            P1W_IsotonicDrinks = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15d_13":
                                        {
                                            P1W_RTD_pkd_coffee = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15d_14":
                                        {
                                            P1W_VitaminDrinks = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15d_15":
                                        {
                                            P1W_FermentedMilk_RTDyoghurt = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15d_16":
                                        {
                                            P1W_TeaBag = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15d_17":
                                        {
                                            P1W_InstantCoffee = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r15d_18":
                                        {
                                            P1W_MilkPowder = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r18":
                                        {
                                            r18 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r16a_1":
                                        {
                                            P3M_Plastic_cup = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r16a_2":
                                        {
                                            P3M_S_PlasticBottle_330ml = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r16a_3":
                                        {
                                            P3M_M_PlasticBottle_600ml = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r16a_4":
                                        {
                                            P3M_L_PlasticBottle_1500ml = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r16a_5":
                                        {
                                            P3M_GlassBottle = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r16a_6":
                                        {
                                            P3M_Gallon = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r16a_7":
                                        {
                                            P3M_PlasticBottle_750ml = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r16b_1":
                                        {
                                            P1M_Plastic_cup = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r16b_2":
                                        {
                                            P1M_S_PlasticBottle_330ml = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r16b_3":
                                        {
                                            P1M_M_PlasticBottle_600ml = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r16b_4":
                                        {
                                            P1M_L_PlasticBottle_1500ml = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r16b_5":
                                        {
                                            P1M_GlassBottle = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r16b_6":
                                        {
                                            P1M_Gallon = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r16b_7":
                                        {
                                            P1M_PlasticBottle_750ml = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r16c_1":
                                        {
                                            P1W_Plastic_cup = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r16c_2":
                                        {
                                            P1W_S_PlasticBottle_330ml = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r16c_3":
                                        {
                                            P1W_M_PlasticBottle_600ml = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r16c_4":
                                        {
                                            P1W_L_PlasticBottle_1500ml = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r16c_5":
                                        {
                                            P1W_GlassBottle = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r16c_6":
                                        {
                                            P1W_Gallon = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r16c_7":
                                        {
                                            P1W_PlasticBottle_750ml = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "r16d":
                                        {
                                            SKU_BUMO = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }

                                    case "q193at11_7":
                                        {
                                            q193at11_7 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at11_17":
                                        {
                                            q193at11_17 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at11_18":
                                        {
                                            q193at11_18 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at11_24":
                                        {
                                            q193at11_24 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at11_26":
                                        {
                                            q193at11_26 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at11_27":
                                        {
                                            q193at11_27 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at12_7":
                                        {
                                            q193at12_7 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at12_17":
                                        {
                                            q193at12_17 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at12_18":
                                        {
                                            q193at12_18 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at12_24":
                                        {
                                            q193at12_24 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at12_26":
                                        {
                                            q193at12_26 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at12_27":
                                        {
                                            q193at12_27 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at13_7":
                                        {
                                            q193at13_7 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at13_17":
                                        {
                                            q193at13_17 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at13_18":
                                        {
                                            q193at13_18 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at13_24":
                                        {
                                            q193at13_24 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at13_26":
                                        {
                                            q193at13_26 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "q193at13_27":
                                        {
                                            q193at13_27 = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23d_12":
                                        {
                                            ConsP1D_LeMinerale = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23d_6":
                                        {
                                            ConsP1D_Aqua = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23d_2":
                                        {
                                            ConsP1D_Ades = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23d_18":
                                        {
                                            ConsP1D_NestlePureLife = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23d_9":
                                        {
                                            ConsP1D_Club = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23d_25":
                                        {
                                            ConsP1D_VIT = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23d_19":
                                        {
                                            ConsP1D_Oasis = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23d_20":
                                        {
                                            ConsP1D_PrimA = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                    case "r23d_7":
                                        {
                                            ConsP1D_Axo = Convert.ToString(record.GetValue(variable));
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    if (u_id != null && Weight != 0)
                    {
                        iobj.insert_Dashboard_values(userID, Weight, country, SurveyID, Gender, MaritalStatus, SURVEY_PERIOD, Location, AgeGroup, Education, PersonalInc, SEC, Period, BrTom, BrSpont_LeMinerale, BrSpont_Aqua, BrSpont_Ades, BrSpont_NestlePureLife, BrSpont_Club, BrSpont_VIT, BrSpont_Oasis, BrSpont_PrimA, BrSpont_Axo, BrAid_LeMinerale, BrAid_Aqua, BrAid_Ades, BrAid_NestlePureLife, BrAid_Club, BrAid_VIT, BrAid_Oasis, BrAid_PrimA, BrAid_Axo, AdTom, AdSpont_LeMinerale, AdSpont_Aqua, AdSpont_Ades, AdSpont_NestlePureLife, AdSpont_Club, AdSpont_VIT, AdSpont_Oasis, AdSpont_PrimA, AdSpont_Axo, AdAid_LeMinerale, AdAid_Aqua, AdAid_Ades, AdAid_NestlePureLife, AdAid_Club, AdAid_VIT, AdAid_Oasis, AdAid_PrimA, AdAid_Axo, EverCons_LeMinerale, EverCons_Aqua, EverCons_Ades, EverCons_NestlePureLife, EverCons_Club, EverCons_VIT, EverCons_Oasis, EverCons_PrimA, EverCons_Axo, ConsP3M_LeMinerale, ConsP3M_Aqua, ConsP3M_Ades, ConsP3M_NestlePureLife, ConsP3M_Club, ConsP3M_VIT, ConsP3M_Oasis, ConsP3M_PrimA, ConsP3M_Axo, ConsP1M_LeMinerale, ConsP1M_Aqua, ConsP1M_Ades, ConsP1M_NestlePureLife, ConsP1M_Club, ConsP1M_VIT, ConsP1M_Oasis, ConsP1M_PrimA, ConsP1M_Axo, ConsP1W_LeMinerale, ConsP1W_Aqua, ConsP1W_Ades, ConsP1W_NestlePureLife, ConsP1W_Club, ConsP1W_VIT, ConsP1W_Oasis, ConsP1W_PrimA, ConsP1W_Axo, frConsP3M_LeMinerale, frConsP3M_Aqua, frConsP3M_Ades, frConsP3M_NestlePureLife, frConsP3M_Club, frConsP3M_VIT, frConsP3M_Oasis, frConsP3M_PrimA, frConsP3M_Axo, Bumo, PreBumo, FavBrand, SecFavBrand, ConToBuy_LeMinerale, ConToBuy_Aqua, ConToBuy_Ades, ConToBuy_NestlePureLife, ConToBuy_Club, ConToBuy_VIT, ConToBuy_Oasis, ConToBuy_PrimA, ConToBuy_Axo, Recommendation, q193at5_7, q193at5_17, q193at5_18, q193at5_24, q193at5_26, q193at5_27, q193at3_7, q193at3_17, q193at3_18, q193at3_24, q193at3_26, q193at3_27, q193at2_7, q193at2_17, q193at2_18, q193at2_24, q193at2_26, q193at2_27, q193at6_7, q193at6_17, q193at6_18, q193at6_24, q193at6_26, q193at6_27, q193at4_7, q193at4_17, q193at4_18, q193at4_24, q193at4_26, q193at4_27, q193at9_7, q193at9_17, q193at9_18, q193at9_24, q193at9_26, q193at9_27, q193at7_7, q193at7_17, q193at7_18, q193at7_24, q193at7_26, q193at7_27, q193at8_7, q193at8_17, q193at8_18, q193at8_24, q193at8_26, q193at8_27, q193at1_7, q193at1_17, q193at1_18, q193at1_24, q193at1_26, q193at1_27, P6M_BottledWater, P6M_SoftDrink, P6M_Juice_RTDjuice, P6M_RTD_tea, P6M_RTD_milk, P6M_EnergyDrinks, P6M_IsotonicDrinks, P6M_RTD_pkd_coffee, P6M_VitaminDrinks, P6M_FermentedMilk_RTDyoghurt, P6M_TeaBag, P6M_InstantCoffee, P6M_MilkPowder, P3M_BottledWater, P3M_SoftDrink, P3M_Juice_RTDjuice, P3M_RTD_tea, P3M_RTD_milk, P3M_EnergyDrinks, P3M_IsotonicDrinks, P3M_RTD_pkd_coffee, P3M_VitaminDrinks, P3M_FermentedMilk_RTDyoghurt, P3M_TeaBag, P3M_InstantCoffee, P3M_MilkPowder, P1M_BottledWater, P1M_SoftDrink, P1M_Juice_RTDjuice, P1M_RTD_tea, P1M_RTD_milk, P1M_EnergyDrinks, P1M_IsotonicDrinks, P1M_RTD_pkd_coffee, P1M_VitaminDrinks, P1M_FermentedMilk_RTDyoghurt, P1M_TeaBag, P1M_InstantCoffee, P1M_MilkPowder, P1W_BottledWater, P1W_SoftDrink, P1W_Juice_RTDjuice, P1W_RTD_tea, P1W_RTD_milk, P1W_EnergyDrinks, P1W_IsotonicDrinks, P1W_RTD_pkd_coffee, P1W_VitaminDrinks, P1W_FermentedMilk_RTDyoghurt, P1W_TeaBag, P1W_InstantCoffee, P1W_MilkPowder, r18, P3M_Plastic_cup, P3M_S_PlasticBottle_330ml, P3M_M_PlasticBottle_600ml, P3M_L_PlasticBottle_1500ml, P3M_GlassBottle, P3M_Gallon, P3M_PlasticBottle_750ml, P1M_Plastic_cup, P1M_S_PlasticBottle_330ml, P1M_M_PlasticBottle_600ml, P1M_L_PlasticBottle_1500ml, P1M_GlassBottle, P1M_Gallon, P1M_PlasticBottle_750ml, P1W_Plastic_cup, P1W_S_PlasticBottle_330ml, P1W_M_PlasticBottle_600ml, P1W_L_PlasticBottle_1500ml, P1W_GlassBottle, P1W_Gallon, P1W_PlasticBottle_750ml, SKU_BUMO, q193at11_7, q193at11_17, q193at11_18, q193at11_24, q193at11_26, q193at11_27, q193at12_7, q193at12_17, q193at12_18, q193at12_24, q193at12_26, q193at12_27, q193at13_7, q193at13_17, q193at13_18, q193at13_24, q193at13_26, q193at13_27, ConsP1D_LeMinerale, ConsP1D_Aqua, ConsP1D_Ades, ConsP1D_NestlePureLife, ConsP1D_Club, ConsP1D_VIT, ConsP1D_Oasis, ConsP1D_PrimA, ConsP1D_Axo);
                        

                    }

                }
            }
        }

        private static string find_UserId(int SurveyID, string SURVEY_PERIOD, string u_id)
        {
            string sum = "";
            string[] date = SURVEY_PERIOD.Split('-');
            foreach (string d in date)
            {
                sum = sum + d;

            }
            return u_id + SurveyID + sum;
        }

    
    }
    class InsertionLeMinerale
    {
        SqlConnection connection = new SqlConnection("Data Source=52.74.59.117;Initial Catalog=ClueboxMobile;Persist Security Info=True;User ID=sa;Password=ClueBox123*;");
        internal void insert_Dashboard_variable_values(string VARIABLE_NAME, string VARIABLE_NAME_SUB_NAME, string VARIABLE_ID, string VARIABLE_VALUE, string VARIABLE_NAME_QUESTION, int SurveyID, string country, string BASE_VARIABLE_NAME, string SURVEY_PERIOD)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO DashboardSPS_Variable_Values (VARIABLE_NAME,VARIABLE_NAME_SUB_NAME,VARIABLE_ID,VARIABLE_VALUE,VARIABLE_NAME_QUESTION,SURVEY_ID,SURVEY_country,BASE_VARIABLE_NAME,SURVEY_PERIOD) " + "VALUES('" + VARIABLE_NAME + "','" + VARIABLE_NAME_SUB_NAME + "','" + VARIABLE_ID + "','" + VARIABLE_VALUE + "','" + VARIABLE_NAME_QUESTION + "','" + SurveyID + "','" + country + "','" + BASE_VARIABLE_NAME + "','" + SURVEY_PERIOD + "')", connection);
            try
            {

                connection.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Dashboard variable values inserted successfully");

                connection.Close();



            }
            catch (Exception)
            {

                Console.WriteLine("Exception occured");
                Console.ReadLine();
            }
        }



        internal void insert_Dashboard_values(string userID, decimal Weight, string country, int SurveyID, string Gender, string MaritalStatus, string SURVEY_PERIOD, string Location, string AgeGroup, string Education, string PersonalInc, string SEC, string Period, string BrTom, string BrSpont_LeMinerale, string BrSpont_Aqua, string BrSpont_Ades, string BrSpont_NestlePureLife, string BrSpont_Club, string BrSpont_VIT, string BrSpont_Oasis, string BrSpont_PrimA, string BrSpont_Axo, string BrAid_LeMinerale, string BrAid_Aqua, string BrAid_Ades, string BrAid_NestlePureLife, string BrAid_Club, string BrAid_VIT, string BrAid_Oasis, string BrAid_PrimA, string BrAid_Axo, string AdTom, string AdSpont_LeMinerale, string AdSpont_Aqua, string AdSpont_Ades, string AdSpont_NestlePureLife, string AdSpont_Club, string AdSpont_VIT, string AdSpont_Oasis, string AdSpont_PrimA, string AdSpont_Axo, string AdAid_LeMinerale, string AdAid_Aqua, string AdAid_Ades, string AdAid_NestlePureLife, string AdAid_Club, string AdAid_VIT, string AdAid_Oasis, string AdAid_PrimA, string AdAid_Axo, string EverCons_LeMinerale, string EverCons_Aqua, string EverCons_Ades, string EverCons_NestlePureLife, string EverCons_Club, string EverCons_VIT, string EverCons_Oasis, string EverCons_PrimA, string EverCons_Axo, string ConsP3M_LeMinerale, string ConsP3M_Aqua, string ConsP3M_Ades, string ConsP3M_NestlePureLife, string ConsP3M_Club, string ConsP3M_VIT, string ConsP3M_Oasis, string ConsP3M_PrimA, string ConsP3M_Axo, string ConsP1M_LeMinerale, string ConsP1M_Aqua, string ConsP1M_Ades, string ConsP1M_NestlePureLife, string ConsP1M_Club, string ConsP1M_VIT, string ConsP1M_Oasis, string ConsP1M_PrimA, string ConsP1M_Axo, string ConsP1W_LeMinerale, string ConsP1W_Aqua, string ConsP1W_Ades, string ConsP1W_NestlePureLife, string ConsP1W_Club, string ConsP1W_VIT, string ConsP1W_Oasis, string ConsP1W_PrimA, string ConsP1W_Axo, string frConsP3M_LeMinerale, string frConsP3M_Aqua, string frConsP3M_Ades, string frConsP3M_NestlePureLife, string frConsP3M_Club, string frConsP3M_VIT, string frConsP3M_Oasis, string frConsP3M_PrimA, string frConsP3M_Axo, string Bumo, string PreBumo, string FavBrand, string SecFavBrand, string ConToBuy_LeMinerale, string ConToBuy_Aqua, string ConToBuy_Ades, string ConToBuy_NestlePureLife, string ConToBuy_Club, string ConToBuy_VIT, string ConToBuy_Oasis, string ConToBuy_PrimA, string ConToBuy_Axo, string Recommendation, string q193at5_7, string q193at5_17, string q193at5_18, string q193at5_24, string q193at5_26, string q193at5_27, string q193at3_7, string q193at3_17, string q193at3_18, string q193at3_24, string q193at3_26, string q193at3_27, string q193at2_7, string q193at2_17, string q193at2_18, string q193at2_24, string q193at2_26, string q193at2_27, string q193at6_7, string q193at6_17, string q193at6_18, string q193at6_24, string q193at6_26, string q193at6_27, string q193at4_7, string q193at4_17, string q193at4_18, string q193at4_24, string q193at4_26, string q193at4_27, string q193at9_7, string q193at9_17, string q193at9_18, string q193at9_24, string q193at9_26, string q193at9_27, string q193at7_7, string q193at7_17, string q193at7_18, string q193at7_24, string q193at7_26, string q193at7_27, string q193at8_7, string q193at8_17, string q193at8_18, string q193at8_24, string q193at8_26, string q193at8_27, string q193at1_7, string q193at1_17, string q193at1_18, string q193at1_24, string q193at1_26, string q193at1_27, string P6M_BottledWater, string P6M_SoftDrink, string P6M_Juice_RTDjuice, string P6M_RTD_tea, string P6M_RTD_milk, string P6M_EnergyDrinks, string P6M_IsotonicDrinks, string P6M_RTD_pkd_coffee, string P6M_VitaminDrinks, string P6M_FermentedMilk_RTDyoghurt, string P6M_TeaBag, string P6M_InstantCoffee, string P6M_MilkPowder, string P3M_BottledWater, string P3M_SoftDrink, string P3M_Juice_RTDjuice, string P3M_RTD_tea, string P3M_RTD_milk, string P3M_EnergyDrinks, string P3M_IsotonicDrinks, string P3M_RTD_pkd_coffee, string P3M_VitaminDrinks, string P3M_FermentedMilk_RTDyoghurt, string P3M_TeaBag, string P3M_InstantCoffee, string P3M_MilkPowder, string P1M_BottledWater, string P1M_SoftDrink, string P1M_Juice_RTDjuice, string P1M_RTD_tea, string P1M_RTD_milk, string P1M_EnergyDrinks, string P1M_IsotonicDrinks, string P1M_RTD_pkd_coffee, string P1M_VitaminDrinks, string P1M_FermentedMilk_RTDyoghurt, string P1M_TeaBag, string P1M_InstantCoffee, string P1M_MilkPowder, string P1W_BottledWater, string P1W_SoftDrink, string P1W_Juice_RTDjuice, string P1W_RTD_tea, string P1W_RTD_milk, string P1W_EnergyDrinks, string P1W_IsotonicDrinks, string P1W_RTD_pkd_coffee, string P1W_VitaminDrinks, string P1W_FermentedMilk_RTDyoghurt, string P1W_TeaBag, string P1W_InstantCoffee, string P1W_MilkPowder, string r18, string P3M_Plastic_cup, string P3M_S_PlasticBottle_330ml, string P3M_M_PlasticBottle_600ml, string P3M_L_PlasticBottle_1500ml, string P3M_GlassBottle, string P3M_Gallon, string P3M_PlasticBottle_750ml, string P1M_Plastic_cup, string P1M_S_PlasticBottle_330ml, string P1M_M_PlasticBottle_600ml, string P1M_L_PlasticBottle_1500ml, string P1M_GlassBottle, string P1M_Gallon, string P1M_PlasticBottle_750ml, string P1W_Plastic_cup, string P1W_S_PlasticBottle_330ml, string P1W_M_PlasticBottle_600ml, string P1W_L_PlasticBottle_1500ml, string P1W_GlassBottle, string P1W_Gallon, string P1W_PlasticBottle_750ml, string SKU_BUMO, string q193at11_7, string q193at11_17, string q193at11_18, string q193at11_24, string q193at11_26, string q193at11_27, string q193at12_7, string q193at12_17, string q193at12_18, string q193at12_24, string q193at12_26, string q193at12_27, string q193at13_7, string q193at13_17, string q193at13_18, string q193at13_24, string q193at13_26, string q193at13_27, string ConsP1D_LeMinerale, string ConsP1D_Aqua, string ConsP1D_Ades, string ConsP1D_NestlePureLife, string ConsP1D_Club, string ConsP1D_VIT, string ConsP1D_Oasis, string ConsP1D_PrimA, string ConsP1D_Axo)
        {
            int i;
            connection.Open();
            //SqlConnection connection = new SqlConnection("Data Source=52.74.59.117;Initial Catalog=ClueboxMobile;Persist Security Info=True;User ID=sa;Password=ClueBox123*;");
            SqlCommand cmd = new SqlCommand("INSERT INTO DashboardFlaTTab_LeMinerale_temp (UserID,Country,SurveyID,Gender,MaritalStatus,AttendedOn,Weight,Location,AgeGroup,Education,PersonalInc,SEC,Period,BrTom,BrSpont_LeMinerale,BrSpont_Aqua,BrSpont_Ades,BrSpont_NestlePureLife,BrSpont_Club,BrSpont_VIT,BrSpont_Oasis,BrSpont_PrimA,BrSpont_Axo,BrAid_LeMinerale,BrAid_Aqua,BrAid_Ades,BrAid_NestlePureLife,BrAid_Club,BrAid_VIT,BrAid_Oasis,BrAid_PrimA,BrAid_Axo,AdTom,AdSpont_LeMinerale,AdSpont_Aqua,AdSpont_Ades,AdSpont_NestlePureLife,AdSpont_Club,AdSpont_VIT,AdSpont_Oasis,AdSpont_PrimA,AdSpont_Axo,AdAid_LeMinerale,AdAid_Aqua,AdAid_Ades,AdAid_NestlePureLife,AdAid_Club,AdAid_VIT,AdAid_Oasis,AdAid_PrimA,AdAid_Axo,EverCons_LeMinerale,EverCons_Aqua,EverCons_Ades,EverCons_NestlePureLife,EverCons_Club,EverCons_VIT,EverCons_Oasis,EverCons_PrimA,EverCons_Axo,ConsP3M_LeMinerale,ConsP3M_Aqua,ConsP3M_Ades,ConsP3M_NestlePureLife,ConsP3M_Club,ConsP3M_VIT,ConsP3M_Oasis,ConsP3M_PrimA,ConsP3M_Axo,ConsP1M_LeMinerale,ConsP1M_Aqua,ConsP1M_Ades,ConsP1M_NestlePureLife,ConsP1M_Club,ConsP1M_VIT,ConsP1M_Oasis,ConsP1M_PrimA,ConsP1M_Axo,ConsP1W_LeMinerale,ConsP1W_Aqua,ConsP1W_Ades,ConsP1W_NestlePureLife,ConsP1W_Club,ConsP1W_VIT,ConsP1W_Oasis,ConsP1W_PrimA,ConsP1W_Axo,frConsP3M_LeMinerale,frConsP3M_Aqua,frConsP3M_Ades,frConsP3M_NestlePureLife,frConsP3M_Club,frConsP3M_VIT,frConsP3M_Oasis,frConsP3M_PrimA,frConsP3M_Axo,Bumo,PreBumo,FavBrand,SecFavBrand,ConToBuy_LeMinerale,ConToBuy_Aqua,ConToBuy_Ades,ConToBuy_NestlePureLife,ConToBuy_Club,ConToBuy_VIT,ConToBuy_Oasis,ConToBuy_PrimA,ConToBuy_Axo,Recommendation,q193at5_7,q193at5_17,q193at5_18,q193at5_24,q193at5_26,q193at5_27,q193at3_7,q193at3_17,q193at3_18,q193at3_24,q193at3_26,q193at3_27,q193at2_7,q193at2_17,q193at2_18,q193at2_24,q193at2_26,q193at2_27,q193at6_7,q193at6_17,q193at6_18,q193at6_24,q193at6_26,q193at6_27,q193at4_7,q193at4_17,q193at4_18,q193at4_24,q193at4_26,q193at4_27,q193at9_7,q193at9_17,q193at9_18,q193at9_24,q193at9_26,q193at9_27,q193at7_7,q193at7_17,q193at7_18,q193at7_24,q193at7_26,q193at7_27,q193at8_7,q193at8_17,q193at8_18,q193at8_24,q193at8_26,q193at8_27,q193at1_7,q193at1_17,q193at1_18,q193at1_24,q193at1_26,q193at1_27,P6M_BottledWater,P6M_SoftDrink,P6M_Juice_RTDjuice,P6M_RTD_tea,P6M_RTD_milk,P6M_EnergyDrinks,P6M_IsotonicDrinks,P6M_RTD_pkd_coffee,P6M_VitaminDrinks,P6M_FermentedMilk_RTDyoghurt,P6M_TeaBag,P6M_InstantCoffee,P6M_MilkPowder,P3M_BottledWater,P3M_SoftDrink,P3M_Juice_RTDjuice,P3M_RTD_tea,P3M_RTD_milk,P3M_EnergyDrinks,P3M_IsotonicDrinks,P3M_RTD_pkd_coffee,P3M_VitaminDrinks,P3M_FermentedMilk_RTDyoghurt,P3M_TeaBag,P3M_InstantCoffee,P3M_MilkPowder,P1M_BottledWater,P1M_SoftDrink,P1M_Juice_RTDjuice,P1M_RTD_tea,P1M_RTD_milk,P1M_EnergyDrinks,P1M_IsotonicDrinks,P1M_RTD_pkd_coffee,P1M_VitaminDrinks,P1M_FermentedMilk_RTDyoghurt,P1M_TeaBag,P1M_InstantCoffee,P1M_MilkPowder,P1W_BottledWater,P1W_SoftDrink,P1W_Juice_RTDjuice,P1W_RTD_tea,P1W_RTD_milk,P1W_EnergyDrinks,P1W_IsotonicDrinks,P1W_RTD_pkd_coffee,P1W_VitaminDrinks,P1W_FermentedMilk_RTDyoghurt,P1W_TeaBag,P1W_InstantCoffee,P1W_MilkPowder,r18,P3M_Plastic_cup,P3M_S_PlasticBottle_330ml,P3M_M_PlasticBottle_600ml,P3M_L_PlasticBottle_1500ml,P3M_GlassBottle,P3M_Gallon,P3M_PlasticBottle_750ml,P1M_Plastic_cup,P1M_S_PlasticBottle_330ml,P1M_M_PlasticBottle_600ml,P1M_L_PlasticBottle_1500ml,P1M_GlassBottle,P1M_Gallon,P1M_PlasticBottle_750ml,P1W_Plastic_cup,P1W_S_PlasticBottle_330ml,P1W_M_PlasticBottle_600ml,P1W_L_PlasticBottle_1500ml,P1W_GlassBottle,P1W_Gallon,P1W_PlasticBottle_750ml,SKU_BUMO,q193at11_7,q193at11_17,q193at11_18,q193at11_24,q193at11_26,q193at11_27,q193at12_7,q193at12_17,q193at12_18,q193at12_24,q193at12_26,q193at12_27,q193at13_7,q193at13_17,q193at13_18,q193at13_24,q193at13_26,q193at13_27,ConsP1D_LeMinerale,ConsP1D_Aqua,ConsP1D_Ades,ConsP1D_NestlePureLife,ConsP1D_Club,ConsP1D_VIT,ConsP1D_Oasis,ConsP1D_PrimA,ConsP1D_Axo) " + "VALUES('" + userID + "','" + country + "','" + SurveyID + "','" + Gender + "','" + MaritalStatus + "','" + SURVEY_PERIOD + "','" + Weight + "','" + Location + "','" + AgeGroup + "','" + Education + "','" + PersonalInc + "','" + SEC + "','" + Period + "','" + BrTom + "','" + BrSpont_LeMinerale + "','" + BrSpont_Aqua + "','" + BrSpont_Ades + "','" + BrSpont_NestlePureLife + "','" + BrSpont_Club + "','" + BrSpont_VIT + "','" + BrSpont_Oasis + "','" + BrSpont_PrimA + "','" + BrSpont_Axo + "','" + BrAid_LeMinerale + "','" + BrAid_Aqua + "','" + BrAid_Ades + "','" + BrAid_NestlePureLife + "','" + BrAid_Club + "','" + BrAid_VIT + "','" + BrAid_Oasis + "','" + BrAid_PrimA + "','" + BrAid_Axo + "','" + AdTom + "','" + AdSpont_LeMinerale + "','" + AdSpont_Aqua + "','" + AdSpont_Ades + "','" + AdSpont_NestlePureLife + "','" + AdSpont_Club + "','" + AdSpont_VIT + "','" + AdSpont_Oasis + "','" + AdSpont_PrimA + "','" + AdSpont_Axo + "','" + AdAid_LeMinerale + "','" + AdAid_Aqua + "','" + AdAid_Ades + "','" + AdAid_NestlePureLife + "','" + AdAid_Club + "','" + AdAid_VIT + "','" + AdAid_Oasis + "','" + AdAid_PrimA + "','" + AdAid_Axo + "','" + EverCons_LeMinerale + "','" + EverCons_Aqua + "','" + EverCons_Ades + "','" + EverCons_NestlePureLife + "','" + EverCons_Club + "','" + EverCons_VIT + "','" + EverCons_Oasis + "','" + EverCons_PrimA + "','" + EverCons_Axo + "','" + ConsP3M_LeMinerale + "','" + ConsP3M_Aqua + "','" + ConsP3M_Ades + "','" + ConsP3M_NestlePureLife + "','" + ConsP3M_Club + "','" + ConsP3M_VIT + "','" + ConsP3M_Oasis + "','" + ConsP3M_PrimA + "','" + ConsP3M_Axo + "','" + ConsP1M_LeMinerale + "','" + ConsP1M_Aqua + "','" + ConsP1M_Ades + "','" + ConsP1M_NestlePureLife + "','" + ConsP1M_Club + "','" + ConsP1M_VIT + "','" + ConsP1M_Oasis + "','" + ConsP1M_PrimA + "','" + ConsP1M_Axo + "','" + ConsP1W_LeMinerale + "','" + ConsP1W_Aqua + "','" + ConsP1W_Ades + "','" + ConsP1W_NestlePureLife + "','" + ConsP1W_Club + "','" + ConsP1W_VIT + "','" + ConsP1W_Oasis + "','" + ConsP1W_PrimA + "','" + ConsP1W_Axo + "','" + frConsP3M_LeMinerale + "','" + frConsP3M_Aqua + "','" + frConsP3M_Ades + "','" + frConsP3M_NestlePureLife + "','" + frConsP3M_Club + "','" + frConsP3M_VIT + "','" + frConsP3M_Oasis + "','" + frConsP3M_PrimA + "','" + frConsP3M_Axo + "','" + Bumo + "','" + PreBumo + "','" + FavBrand + "','" + SecFavBrand + "','" + ConToBuy_LeMinerale + "','" + ConToBuy_Aqua + "','" + ConToBuy_Ades + "','" + ConToBuy_NestlePureLife + "','" + ConToBuy_Club + "','" + ConToBuy_VIT + "','" + ConToBuy_Oasis + "','" + ConToBuy_PrimA + "','" + ConToBuy_Axo + "','" + Recommendation + "','" + q193at5_7 + "','" + q193at5_17 + "','" + q193at5_18 + "','" + q193at5_24 + "','" + q193at5_26 + "','" + q193at5_27 + "','" + q193at3_7 + "','" + q193at3_17 + "','" + q193at3_18 + "','" + q193at3_24 + "','" + q193at3_26 + "','" + q193at3_27 + "','" + q193at2_7 + "','" + q193at2_17 + "','" + q193at2_18 + "','" + q193at2_24 + "','" + q193at2_26 + "','" + q193at2_27 + "','" + q193at6_7 + "','" + q193at6_17 + "','" + q193at6_18 + "','" + q193at6_24 + "','" + q193at6_26 + "','" + q193at6_27 + "','" + q193at4_7 + "','" + q193at4_17 + "','" + q193at4_18 + "','" + q193at4_24 + "','" + q193at4_26 + "','" + q193at4_27 + "','" + q193at9_7 + "','" + q193at9_17 + "','" + q193at9_18 + "','" + q193at9_24 + "','" + q193at9_26 + "','" + q193at9_27 + "','" + q193at7_7 + "','" + q193at7_17 + "','" + q193at7_18 + "','" + q193at7_24 + "','" + q193at7_26 + "','" + q193at7_27 + "','" + q193at8_7 + "','" + q193at8_17 + "','" + q193at8_18 + "','" + q193at8_24 + "','" + q193at8_26 + "','" + q193at8_27 + "','" + q193at1_7 + "','" + q193at1_17 + "','" + q193at1_18 + "','" + q193at1_24 + "','" + q193at1_26 + "','" + q193at1_27 + "','" + P6M_BottledWater + "','" + P6M_SoftDrink + "','" + P6M_Juice_RTDjuice + "','" + P6M_RTD_tea + "','" + P6M_RTD_milk + "','" + P6M_EnergyDrinks + "','" + P6M_IsotonicDrinks + "','" + P6M_RTD_pkd_coffee + "','" + P6M_VitaminDrinks + "','" + P6M_FermentedMilk_RTDyoghurt + "','" + P6M_TeaBag + "','" + P6M_InstantCoffee + "','" + P6M_MilkPowder + "','" + P3M_BottledWater + "','" + P3M_SoftDrink + "','" + P3M_Juice_RTDjuice + "','" + P3M_RTD_tea + "','" + P3M_RTD_milk + "','" + P3M_EnergyDrinks + "','" + P3M_IsotonicDrinks + "','" + P3M_RTD_pkd_coffee + "','" + P3M_VitaminDrinks + "','" + P3M_FermentedMilk_RTDyoghurt + "','" + P3M_TeaBag + "','" + P3M_InstantCoffee + "','" + P3M_MilkPowder + "','" + P1M_BottledWater + "','" + P1M_SoftDrink + "','" + P1M_Juice_RTDjuice + "','" + P1M_RTD_tea + "','" + P1M_RTD_milk + "','" + P1M_EnergyDrinks + "','" + P1M_IsotonicDrinks + "','" + P1M_RTD_pkd_coffee + "','" + P1M_VitaminDrinks + "','" + P1M_FermentedMilk_RTDyoghurt + "','" + P1M_TeaBag + "','" + P1M_InstantCoffee + "','" + P1M_MilkPowder + "','" + P1W_BottledWater + "','" + P1W_SoftDrink + "','" + P1W_Juice_RTDjuice + "','" + P1W_RTD_tea + "','" + P1W_RTD_milk + "','" + P1W_EnergyDrinks + "','" + P1W_IsotonicDrinks + "','" + P1W_RTD_pkd_coffee + "','" + P1W_VitaminDrinks + "','" + P1W_FermentedMilk_RTDyoghurt + "','" + P1W_TeaBag + "','" + P1W_InstantCoffee + "','" + P1W_MilkPowder + "','" + r18 + "','" + P3M_Plastic_cup + "','" + P3M_S_PlasticBottle_330ml + "','" + P3M_M_PlasticBottle_600ml + "','" + P3M_L_PlasticBottle_1500ml + "','" + P3M_GlassBottle + "','" + P3M_Gallon + "','" + P3M_PlasticBottle_750ml + "','" + P1M_Plastic_cup + "','" + P1M_S_PlasticBottle_330ml + "','" + P1M_M_PlasticBottle_600ml + "','" + P1M_L_PlasticBottle_1500ml + "','" + P1M_GlassBottle + "','" + P1M_Gallon + "','" + P1M_PlasticBottle_750ml + "','" + P1W_Plastic_cup + "','" + P1W_S_PlasticBottle_330ml + "','" + P1W_M_PlasticBottle_600ml + "','" + P1W_L_PlasticBottle_1500ml + "','" + P1W_GlassBottle + "','" + P1W_Gallon + "','" + P1W_PlasticBottle_750ml + "','" + SKU_BUMO + "','" + q193at11_7 + "','" + q193at11_17 + "','" + q193at11_18 + "','" + q193at11_24 + "','" + q193at11_26 + "','" + q193at11_27 + "','" + q193at12_7 + "','" + q193at12_17 + "','" + q193at12_18 + "','" + q193at12_24 + "','" + q193at12_26 + "','" + q193at12_27 + "','" + q193at13_7 + "','" + q193at13_17 + "','" + q193at13_18 + "','" + q193at13_24 + "','" + q193at13_26 + "','" + q193at13_27 + "','" + ConsP1D_LeMinerale + "','" + ConsP1D_Aqua + "','" + ConsP1D_Ades + "','" + ConsP1D_NestlePureLife + "','" + ConsP1D_Club + "','" + ConsP1D_VIT + "','" + ConsP1D_Oasis + "','" + ConsP1D_PrimA + "','" + ConsP1D_Axo + "')", connection);
            try
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("Data inserted successfully");
                i = 1;
            }
            catch (Exception ex)
            {

                connection.Close();
                i = 0;
                Console.WriteLine("Exception occured" + ex.ToString());
                Console.WriteLine("Exception occured");

                Console.ReadLine();
            }
            connection.Close();
        }

        
    }
}
