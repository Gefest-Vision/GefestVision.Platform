using System.Collections;

namespace GefestVision.Core.Contracts;

public class TwinCollection : IEnumerable<KeyValuePair<string, object>>
{
    
    
    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
