namespace Assets.Script.Data
{
    public class PokeResponse
    {
        public int code;
        public string msg;
    }

    public delegate void SuccessCallback(string data);
    public delegate void FailureCallback(int code, string errorMessage);

    public sealed class ResponseCallbacks
    {
        public ResponseCallbacks(SuccessCallback loginSuccessCallback)
        {
            SuccessCallback = loginSuccessCallback;
        }
        public ResponseCallbacks(SuccessCallback loginSuccessCallback, FailureCallback loginFailureCallback)
        {
            SuccessCallback = loginSuccessCallback;
            FailureCallback = loginFailureCallback;
        }
        //
        // Summary:
        //     获取加载资源成功回调函数。
        public SuccessCallback SuccessCallback { get; }
        //
        // Summary:
        //     获取加载资源失败回调函数。
        public FailureCallback FailureCallback { get; }
    }
}