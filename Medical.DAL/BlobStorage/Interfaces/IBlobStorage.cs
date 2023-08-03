namespace Medical.DAL.BlobStorage.Interfaces
{
    public interface IBlobStorage
    {
        Task PutContextAsync(string filename);
        Task<bool> ContainsFileByNameAsync(string toString);
        List<int> FindDoctorByHospitalId(Guid hospitalId);
    }
}
