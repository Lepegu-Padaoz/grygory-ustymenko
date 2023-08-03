namespace Medical.BL.Exceptions
{
    // Exceptions which occurres when some entity doesn't exist in storage/db
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message): base(message) { }
    }
}
