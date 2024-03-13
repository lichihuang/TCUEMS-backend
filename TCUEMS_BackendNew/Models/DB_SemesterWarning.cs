namespace TCUEMS_BackendNew.Models
{
    public class SemesterWarning
    {
        public  string w_smtr { get; set; }
        public string? w_std_no { get; set; }
        public string? chi_name { get; set; }
        public string? st_state { get; set; }
        public  string dept_name_s { get; set; }
        public string? degree { get; set; }
        public string? sw_class { get; set; }

        public bool IsValid()
        {
            // 在這裡添加你的檢查邏輯，檢查是否包含不正確的數據
            // 如果數據有效，返回 true；否則返回 false
            return !string.IsNullOrEmpty(dept_name_s) && !string.IsNullOrEmpty(w_smtr);
        }
    }
}
