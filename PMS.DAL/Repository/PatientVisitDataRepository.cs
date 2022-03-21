using PMS.Domain.DTO;
using PMS.Domain.Entiites;
using PMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL.Repository
{
    public class PatientVisitDataRepository : GenericRepository<PatientVisitDetails>, IPatientVisitDataRepository
    {
        public PatientVisitDataRepository(ApplicationDBContext context) : base(context)
        {

        }       

        public List<PatientVisits> GetPatientVisitHistoryList(int patientId)
        {
            List<PatientVisits> visitDetails = new List<PatientVisits>();
            try
            {
                //PatientVisitDetails visits = new PatientVisitDetails();
                var patientvisits = _context.PatientVisits.Where(d => d.UserId == patientId).OrderByDescending(d=>d.Visit_Date).ToList();

                foreach (var item in patientvisits)
                {   
                    visitDetails.Add(item);
                }
            }
            catch
            {
                throw;
            }

            return visitDetails;
        }

        public List<PatientVisitsDTO> GetPatientVisitHistoryListForPhysician()
        {
            List<PatientVisitsDTO> visitDetails = new List<PatientVisitsDTO>();
            try
            {
                var patientvisits = _context.PatientVisits.OrderByDescending(d => d.Visit_Date).ToList();

                foreach (var item in patientvisits)
                {
                    var userName = _context.Registration.Where(d => d.UserId == item.UserId).FirstOrDefault();                    
                    PatientVisitsDTO data = new PatientVisitsDTO();
                    data.Visit_Id = item.Visit_Id;
                    data.Visit_Date = item.Visit_Date;
                    data.UserId = item.UserId;
                    data.Height = item.Height;
                    data.Weight = item.Weight;
                    data.Blood_Pressure = item.Blood_Pressure;
                    data.Body_Temperature = item.Body_Temperature;
                    data.Respiration_Rate = item.Respiration_Rate;
                        data.UserName = userName.FirstName + " " + userName.LastName;
                    visitDetails.Add(data);
                }
            }
            catch
            {
                throw;
            }

            return visitDetails;
        }

        public PatientVisitDetails GetPatientVisitDetails(int visitId)
        {
            PatientVisitDetails visitDetails = new PatientVisitDetails();
            try
            {
                //for visit details 
                var patientvisits = _context.PatientVisits.Where(d => d.Visit_Id == visitId).FirstOrDefault();

                if (patientvisits != null)
                {
                    visitDetails.patientVisits = patientvisits;
                    visitDetails.diagnoses = new List<PatientVisitDiagnosisDTO>();
                    visitDetails.procedures = new List<PatientVisitProceduresDTO>();
                    visitDetails.medications = new List<PatientVisitMedicationDTO>();

                    //for diagnosis
                    var diagnosisdata = _context.PatientVisitDiagnoses.Where(d => d.Visit_Id == patientvisits.Visit_Id).ToList();
                    foreach (var diagnosis in diagnosisdata)
                    {
                        PatientVisitDiagnosisDTO diagnosisDTO = new PatientVisitDiagnosisDTO();
                        var diagnosistbl = _context.Diagnoses.Where(d => d.Diagnosis_Id == diagnosis.Diagnosis_Id).FirstOrDefault();
                        diagnosisDTO.Visit_Id = diagnosis.Visit_Id;
                        diagnosisDTO.Diagnosis_Id = diagnosis.Diagnosis_Id;
                        diagnosisDTO.Diagnosis_Code = diagnosistbl.Diagnosis_Code;
                        diagnosisDTO.Diagnosis_Description = diagnosistbl.Diagnosis_Description;
                        diagnosisDTO.Diagnosis_Is_Depricated = diagnosistbl.Diagnosis_Is_Depricated;
                        diagnosisDTO.Note = diagnosis.Note;
                        visitDetails.diagnoses.Add(diagnosisDTO);
                    }

                    //for procedures
                    var proceduredata = _context.PatientVisitProcedures.Where(d => d.Visit_Id == patientvisits.Visit_Id).ToList();
                    foreach (var procedure in proceduredata)
                    {
                        PatientVisitProceduresDTO proceduresDTO = new PatientVisitProceduresDTO();
                        var proceduretbl = _context.Procedures.Where(d => d.Procedure_ID == procedure.Procedure_Id).FirstOrDefault();
                        proceduresDTO.Visit_Id = procedure.Visit_Id;
                        proceduresDTO.Procedure_Id = procedure.Procedure_Id;
                        proceduresDTO.Procedure_Code = proceduretbl.Procedure_Code;
                        proceduresDTO.Procedure_Description = proceduretbl.Procedure_Description;
                        proceduresDTO.Procedure__Is_Depricated = proceduretbl.Procedure_Is_Depricated;
                        proceduresDTO.Note = procedure.Note;
                        visitDetails.procedures.Add(proceduresDTO);
                    }

                    //for medication
                    var medicationdata = _context.PatientVisitMedications.Where(d => d.Visit_Id == patientvisits.Visit_Id).ToList();
                    foreach (var medication in medicationdata)
                    {
                        PatientVisitMedicationDTO medicationDTO = new PatientVisitMedicationDTO();
                        var medicationtbl = _context.DrugDatas.Where(d => d.Drug_ID == medication.Drug_Id).FirstOrDefault();
                        medicationDTO.Visit_Id = medication.Visit_Id;
                        medicationDTO.Drug_Id = medicationtbl.Drug_ID;
                        medicationDTO.Drug_Name = medicationtbl.Drug_Name;
                        medicationDTO.Drug_Generic_Name = medicationtbl.Drug_Generic_Name;
                        medicationDTO.Drug_Form = medicationtbl.Drug_Form;
                        medicationDTO.Dosage = medication.Dosage;
                        medicationDTO.Note = medication.Note;
                        visitDetails.medications.Add(medicationDTO);
                    }
                }                
            }
            catch
            {
                throw;
            }

            return visitDetails;
        }

        public int AddPatientVisit(PatientVisits visit)
        {
            int visitId = 0;

            try
            {
                PatientVisits patientVisits = new PatientVisits();
                //patientVisits.Visit_Date = visit.Visit_Date;
                patientVisits.Visit_Date = DateTime.Now;
                patientVisits.Height = visit.Height;
                patientVisits.Weight = visit.Weight;
                patientVisits.Blood_Pressure = visit.Blood_Pressure;
                patientVisits.Body_Temperature = visit.Body_Temperature;
                patientVisits.Respiration_Rate = visit.Respiration_Rate;
                patientVisits.UserId = visit.UserId;
                patientVisits.CreatedOn = DateTime.Now;
                patientVisits.Note = visit.Note;
                _context.PatientVisits.Add(patientVisits);
                _context.SaveChanges();
                visitId = patientVisits.Visit_Id;
            }
            catch
            {
                return visitId;
            }
            return visitId;
        }

        public int AddVisitDiagnosis(List<PatientVisitDiagnosis> diagnosis)
        {
            int save = 0;

            try
            {

                var visitdiagnosis = _context.PatientVisitDiagnoses.Where(d => d.Visit_Id == diagnosis.FirstOrDefault().Visit_Id).ToList();
                foreach (var item in visitdiagnosis)
                {  
                    _context.PatientVisitDiagnoses.Remove(item);
                    _context.SaveChanges();
                }

                foreach (var item in diagnosis)
                {
                    PatientVisitDiagnosis patientDiagnosis = new PatientVisitDiagnosis();
                    patientDiagnosis.Visit_Id = item.Visit_Id;
                    patientDiagnosis.Diagnosis_Id = item.Diagnosis_Id;
                    patientDiagnosis.Description = item.Description;
                    patientDiagnosis.Note = item.Note;
                    _context.PatientVisitDiagnoses.Add(patientDiagnosis);
                }

              
                _context.SaveChanges();
                save = (int)diagnosis.FirstOrDefault().Visit_Id;
            }
            catch
            {
                return save;
            }
            return save;
        }

        public int AddVisitProcedures(List<PatientVisitProcedures> procedures)
        {
            int save = 0;

            try
            {

                var visitprocedures = _context.PatientVisitProcedures.Where(d => d.Visit_Id == procedures.FirstOrDefault().Visit_Id).ToList();
                foreach (var item in visitprocedures)
                {
                    _context.PatientVisitProcedures.Remove(item);
                    _context.SaveChanges();
                }

                foreach (var item in procedures)
                {
                    PatientVisitProcedures patientProcedures = new PatientVisitProcedures();
                    patientProcedures.Visit_Id = item.Visit_Id;
                    patientProcedures.Procedure_Id = item.Procedure_Id;
                    patientProcedures.Description = item.Description;
                    patientProcedures.Note = item.Note;
                    _context.PatientVisitProcedures.Add(patientProcedures);
                }

                _context.SaveChanges();
                save = (int)procedures.FirstOrDefault().Visit_Id;
            }
            catch
            {
                return save;
            }
            return save;
        }

        public int AddVisitMedications(List<PatientVisitMedication> medication)
        {
            int save = 0;

            try
            {
                var visitmedication = _context.PatientVisitMedications.Where(d => d.Visit_Id == medication.FirstOrDefault().Visit_Id).ToList();
                foreach (var item in visitmedication)
                {
                    _context.PatientVisitMedications.Remove(item);
                    _context.SaveChanges();
                }

                foreach (var item in medication)
                {
                    PatientVisitMedication patientMedication = new PatientVisitMedication();
                    patientMedication.Visit_Id = item.Visit_Id;
                    patientMedication.Drug_Id = item.Drug_Id;
                    patientMedication.Dosage = item.Dosage;
                    patientMedication.Description = item.Description;
                    patientMedication.Note = item.Note;
                    _context.PatientVisitMedications.Add(patientMedication);
                }

                _context.SaveChanges();
                save = (int)medication.FirstOrDefault().Visit_Id;
            }
            catch
            {
                return save;
            }
            return save;
        }
    }
}
