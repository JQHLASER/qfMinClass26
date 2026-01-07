using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qf_Laser
{

    public enum _Err_jczMarkEzd2_
    {
        成功 = 0,
        发现EZCAD在运行 = 1,
        找不到EZCAD_CFG文件 = 2,
        打开LMC1失败 = 3,
        没有有效的lmc1设备 = 4,
        lmc1版本错误 = 5,
        找不到设备配置文件 = 6,
        报警信号 = 7,
        用户停止 = 8,
        不明错误 = 9,
        超时 = 10,
        未初始化 = 11,
        读文件错误 = 12,
        窗口为空 = 13,
        找不到指定名称的字体 = 14,
        错误的笔号 = 15,
        指定名称的对象不是文本对象 = 16,
        保存文件失败 = 17,
        找不到指定对象 = 18,
        当前状态下不能执行此操作 = 19,
        硬件不可开发 = 21,

        端口不在有效范置 = 97,
        未找到ezd文件 = 98,
        DL故障 = 99,

    }

}
