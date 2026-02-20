# SupeedTOTP 使用与开发指南

## 项目概述

**SupeedTOTP** 是一个基于 C# 和 WPF 开发的 TOTP（Time-based One-Time Password）令牌生成器，专为 Windows 平台设计，提供安全、可靠的一次性密码生成功能。

### 核心功能
- ✅ TOTP 令牌生成与验证
- ✅ 支持多种哈希算法（SHA1, SHA256, SHA512）
- ✅ 自定义时间步长和令牌长度
- ✅ 账号信息管理（增删改查）
- ✅ 数据加密存储
- ✅ 一键复制令牌
- ✅ 令牌自动刷新

## 快速开始

### 前提条件
- **.NET 8.0 SDK** 或更高版本
- **Windows 10** 或更高版本
- **Visual Studio 2022** 或更高版本（可选，用于开发）

### 安装与运行

#### 方法 1：从源代码运行

```bash
# 克隆仓库
git clone https://github.com/Ink-dark/SupeedTOTP.git
cd SupeedTOTP

# 构建项目
dotnet build

# 运行应用
dotnet run --project SupeedTOTP.UI/SupeedTOTP.UI.csproj
```

#### 方法 2：发布版本

```bash
# 发布应用
dotnet publish SupeedTOTP.UI/SupeedTOTP.UI.csproj -c Release -r win-x64 --self-contained true

# 运行发布后的应用
cd SupeedTOTP.UI/bin/Release/net8.0-windows/win-x64/publish
SupeedTOTP.UI.exe
```

## 使用指南

### 基本操作

#### 1. 添加账号
1. 点击 "添加账号" 按钮
2. 在弹出的表单中填写以下信息：
   - **账号名称**：您的账号标识（如 "GitHub"、"Google" 等）
   - **服务商**：服务提供商名称
   - **密钥**：TOTP 密钥（Base32 格式）
   - **哈希算法**：选择 SHA1、SHA256 或 SHA512
   - **令牌长度**：选择 6-8 位
   - **时间步长**：默认 30 秒
3. 点击 "保存" 完成添加

#### 2. 查看和使用令牌
- **令牌显示**：主界面会显示所有账号的当前令牌
- **自动刷新**：令牌会根据时间步长自动刷新
- **一键复制**：点击账号卡片上的 "复制" 按钮，令牌会自动复制到剪贴板
- **剩余时间**：每个令牌下方会显示剩余有效时间

#### 3. 管理账号
- **编辑账号**：双击账号卡片进入编辑模式
- **删除账号**：右键点击账号卡片，选择 "删除"
- **搜索账号**：在顶部搜索框中输入关键词

### 高级功能

#### 数据备份与恢复
- **备份数据**：点击 "设置" → "备份数据"，选择保存位置
- **恢复数据**：点击 "设置" → "恢复数据"，选择备份文件

#### 导入/导出
- **导入账号**：支持从 CSV 文件或 QR 码导入
- **导出账号**：支持导出为 CSV 文件或生成 QR 码

#### 安全性设置
- **主密码**：设置应用启动密码
- **自动锁定**：设置闲置自动锁定时间
- **剪贴板设置**：配置剪贴板自动清除时间

## 开发指南

### 项目结构

```
SupeedTOTP/
├── SupeedTOTP.Core/        # 核心业务逻辑
│   ├── Models/            # 数据模型
│   └── Services/          # 业务服务
├── SupeedTOTP.Data/        # 数据访问层
│   ├── Repositories/      # 数据仓库
│   └── AppDbContext.cs    # 数据库上下文
├── SupeedTOTP.UI/          # WPF 用户界面
│   ├── ViewModels/        # 视图模型
│   ├── MainWindow.xaml     # 主窗口
│   └── App.xaml           # 应用程序配置
├── SupeedTOTP.Tests/       # 单元测试
└── SupeedTOTP.sln         # 解决方案文件
```

### 核心组件

#### 1. TotpService

**功能**：实现 TOTP 算法的核心服务

**主要方法**：
- `GenerateTotp(Account account)`：生成 TOTP 令牌
- `VerifyTotp(Account account, string code)`：验证 TOTP 令牌
- `GetRemainingSeconds(Account account)`：获取剩余有效时间

**使用示例**：
```csharp
var account = new Account {
    Secret = "JBSWY3DPEHPK3PXP",
    Period = 30,
    Digits = 6
};

var totpService = new TotpService();
string token = totpService.GenerateTotp(account);
int remainingSeconds = totpService.GetRemainingSeconds(account);
```

#### 2. AccountRepository

**功能**：账号数据的持久化管理

**主要方法**：
- `AddAsync(Account account)`：添加账号
- `UpdateAsync(Account account)`：更新账号
- `DeleteAsync(Guid id)`：删除账号
- `GetAllAsync()`：获取所有账号
- `SearchAsync(string query)`：搜索账号

#### 3. ViewModels

**MainViewModel**：管理应用程序主界面状态
- 账号列表管理
- 搜索功能
- 命令处理

**AccountViewModel**：管理单个账号的状态
- 令牌生成和刷新
- 剩余时间计算
- 复制令牌功能

### 开发流程

#### 1. 环境搭建
1. 安装 **Visual Studio 2022** 或更高版本
2. 安装 **.NET 8.0 SDK**
3. 克隆代码仓库
4. 在 Visual Studio 中打开 `SupeedTOTP.sln`

#### 2. 添加新功能

**步骤 1：定义需求**
- 明确功能需求和技术实现方案

**步骤 2：实现核心逻辑**
- 在 `SupeedTOTP.Core` 中添加新的服务或修改现有服务

**步骤 3：更新数据模型**
- 如需修改数据结构，更新 `Account` 模型
- 添加数据库迁移（如需要）

**步骤 4：实现 UI**
- 在 `SupeedTOTP.UI` 中添加或修改 ViewModels
- 更新 XAML 文件添加 UI 元素
- 实现命令绑定和事件处理

**步骤 5：测试**
- 运行应用程序测试新功能
- 编写单元测试（可选）

### 常见问题与解决方案

#### 1. 应用程序启动失败

**症状**：应用程序无法启动，显示错误信息

**解决方案**：
- 检查 .NET 8.0 SDK 是否正确安装
- 检查 Windows 版本是否满足要求
- 查看应用程序日志获取详细错误信息

#### 2. 令牌生成错误

**症状**：生成的令牌与其他 TOTP 应用不一致

**解决方案**：
- 检查账号的密钥是否正确
- 确认哈希算法设置是否一致
- 验证时间步长和令牌长度设置

#### 3. 数据存储问题

**症状**：账号数据丢失或无法保存

**解决方案**：
- 检查应用程序是否有文件系统写入权限
- 尝试使用 "备份数据" 功能保存当前数据
- 检查数据库文件是否损坏

#### 4. UI 响应缓慢

**症状**：界面卡顿或响应延迟

**解决方案**：
- 减少同时显示的账号数量
- 关闭不必要的后台应用
- 检查系统资源使用情况

## 技术栈

| 类别 | 技术/库 | 版本 | 用途 |
|------|---------|------|------|
| 开发语言 | C# | 10.0+ | 核心开发语言 |
| 框架 | .NET | 8.0 | 运行时框架 |
| UI 框架 | WPF | - | 用户界面 |
| TOTP 算法 | Otp.NET | 1.4.0 | TOTP 实现 |
| 数据存储 | SQLite | - | 本地数据存储 |
| ORM | Entity Framework Core | 8.0.0 | 数据库操作 |
| 加密 | BouncyCastle | - | 数据加密 |

## 性能优化

### 1. 令牌生成优化
- 使用内存缓存减少重复计算
- 批量处理账号刷新
- 优化时间同步机制

### 2. UI 性能
- 使用虚拟化列表减少内存使用
- 延迟加载非关键 UI 元素
- 优化数据绑定更新频率

### 3. 数据访问
- 使用异步操作避免 UI 阻塞
- 实现数据缓存减少数据库访问
- 优化查询语句提高检索速度

## 安全最佳实践

### 1. 数据安全
- 敏感数据加密存储
- 使用强密码哈希算法
- 定期备份数据

### 2. 应用安全
- 实现主密码保护
- 添加自动锁定功能
- 安全处理剪贴板数据

### 3. 代码安全
- 输入验证防止注入攻击
- 安全的密钥管理
- 遵循最小权限原则

## 部署与分发

### 构建发布版本

```bash
# 构建发布版本
dotnet publish SupeedTOTP.UI/SupeedTOTP.UI.csproj -c Release -r win-x64 --self-contained true

# 构建便携版本
dotnet publish SupeedTOTP.UI/SupeedTOTP.UI.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

### 安装程序创建

1. 使用 **Visual Studio Installer Projects** 扩展
2. 配置安装选项和快捷方式
3. 构建 MSI 安装包

## 贡献指南

### 提交代码

1. **Fork** 仓库
2. 创建功能分支
3. 提交更改
4. 创建 Pull Request

### 代码规范

- 遵循 **C# 编码规范**
- 使用 ** nullable reference types**
- 编写清晰的注释
- 实现适当的错误处理

### 报告问题

- 在 GitHub Issues 中提交详细的问题描述
- 包含复现步骤和错误信息
- 提供系统环境信息

## 故障排除

### 日志查看

应用程序会在控制台输出详细日志，可通过以下方式查看：

```bash
# 在命令行中运行并查看日志
dotnet run --project SupeedTOTP.UI/SupeedTOTP.UI.csproj
```

### 常见错误代码

| 错误代码 | 描述 | 解决方案 |
|----------|------|----------|
| 0x001 | 无法加载配置文件 | 检查配置文件权限 |
| 0x002 | 数据库连接失败 | 检查文件系统权限 |
| 0x003 | 密钥格式错误 | 验证 Base32 编码的密钥 |
| 0x004 | 时间同步问题 | 检查系统时间设置 |

## 联系与支持

### 联系方式
- **GitHub**：[https://github.com/Ink-dark/SupeedTOTP](https://github.com/Ink-dark/SupeedTOTP)
- **Email**：moranqidarkseven@hallochat.cn

### 支持渠道
- GitHub Issues：提交 bug 报告和功能请求
- 讨论区：参与项目讨论和问题解答

## 版本历史

| 版本 | 发布日期 | 主要变更 |
|------|----------|----------|
| v0.1.0 | 2026-02-20 | 从 Avalonia 迁移到 WPF，修复启动崩溃问题 |
| v0.0.0 | 2026-02-19 | 首次发布，基础 TOTP 功能 |

---

**SupeedTOTP** - 安全、可靠的 Windows TOTP 令牌生成器

*本指南会定期更新，以反映最新的功能和最佳实践。*
