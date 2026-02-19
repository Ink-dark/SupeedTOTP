# SupeedTOTP

SupeedTOTP 是一个基于 C# 语言开发的跨平台 TOTP（Time-based One-Time Password）令牌生成软件，支持 Windows、Linux 和 macOS 操作系统。

## 功能特性

### 核心功能
- ✅ TOTP 令牌生成与验证
- ✅ 支持多种哈希算法（SHA1, SHA256, SHA512）
- ✅ 自定义时间步长（默认30秒）
- ✅ 支持6-8位令牌长度

### 数据管理
- ✅ 账号信息的增删改查
- ✅ 数据加密存储
- ✅ 数据备份与恢复
- ✅ 导入/导出功能（支持标准格式如 QR 码、CSV）

### 用户界面
- ✅ 主界面显示所有令牌
- ✅ 一键复制令牌
- ✅ 令牌自动刷新
- ✅ 搜索和分类功能
- ✅ 深色/浅色主题切换

### 安全性
- ✅ 主密码保护
- ✅ 自动锁定功能
- ✅ 剪贴板自动清除
- ✅ 敏感数据加密存储

## 技术栈

- **开发语言**: C# 10.0+
- **跨平台框架**: .NET 8.0
- **UI 框架**: Avalonia UI 11.0
- **TOTP 算法库**: Otp.NET
- **数据存储**: SQLite + Entity Framework Core
- **加密库**: BouncyCastle

## 项目结构

```
SupeedTOTP/
├── SupeedTOTP.Core/           # 核心业务逻辑
│   ├── Models/               # 数据模型
│   ├── Services/             # 业务服务
│   ├── Utilities/            # 工具类
│   └── Interfaces/           # 接口定义
├── SupeedTOTP.UI/            # UI 层（Avalonia）
│   ├── Views/               # 视图
│   ├── ViewModels/          # 视图模型
│   ├── Controls/            # 自定义控件
│   └── Resources/           # 资源文件
├── SupeedTOTP.Data/          # 数据访问层
│   ├── Repositories/        # 仓库
│   └── Migrations/          # 数据库迁移
├── SupeedTOTP.Tests/         # 单元测试
├── SupeedTOTP.sln           # 解决方案文件
└── README.md               # 项目说明
```

## 构建与运行

### 前提条件
- 安装 [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### 构建项目

```bash
# 克隆仓库
git clone https://github.com/yourusername/SupeedTOTP.git
cd SupeedTOTP

# 构建解决方案
dotnet build SupeedTOTP.sln -c Release
```

### 运行应用

```bash
# 运行 UI 项目
dotnet run --project SupeedTOTP.UI/SupeedTOTP.UI.csproj
```

### 发布应用

```bash
# Windows
dotnet publish SupeedTOTP.UI/SupeedTOTP.UI.csproj -c Release -r win-x64 --self-contained true

# Linux
dotnet publish SupeedTOTP.UI/SupeedTOTP.UI.csproj -c Release -r linux-x64 --self-contained true

# macOS
dotnet publish SupeedTOTP.UI/SupeedTOTP.UI.csproj -c Release -r osx-x64 --self-contained true
```

## 使用指南

### 添加账号

1. 点击 "添加账号" 按钮
2. 输入账号名称、服务商、密钥等信息
3. 选择哈希算法、令牌长度和时间步长
4. 点击 "保存" 完成添加

### 复制令牌

1. 在令牌列表中找到需要的账号
2. 点击 "复制" 按钮，令牌将自动复制到剪贴板
3. 剪贴板内容将在30秒后自动清除

### 搜索账号

1. 在搜索框中输入关键词
2. 账号列表将实时根据关键词过滤

### 设置

1. 点击 "设置" 按钮进入设置界面
2. 可以设置主密码、自动锁定时间、主题等

## 安全性

- 所有敏感数据均采用 AES-256 加密存储
- 支持主密码保护，防止未经授权访问
- 自动锁定功能，闲置一段时间后自动锁定
- 剪贴板自动清除，保护令牌安全

## 许可证

SupeedTOTP 采用 MIT 许可证，详见 [LICENSE](LICENSE) 文件。

## 贡献

欢迎提交 Issue 和 Pull Request 来帮助改进 SupeedTOTP！

## 联系方式

如有问题或建议，请通过以下方式联系：

- GitHub: [https://github.com/Ink-dark/SupeedTOTP](https://github.com/Ink-dark/SupeedTOTP)
- Email: moranqidarkseven@hallochat.cn

## 更新日志

### v0.0.0 (2026-02-19)
- 首次发布
- 支持 TOTP 令牌生成与验证
- 支持多种哈希算法
- 支持跨平台运行
- 支持账号管理功能

---

**SupeedTOTP** - 安全、可靠的跨平台 TOTP 令牌生成器
