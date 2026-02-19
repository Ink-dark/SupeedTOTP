# 确保在SupeedTOTP目录下执行
$projectPath = "d:\CodeWorkspace\SupeedTOTP"
Set-Location $projectPath

# 初始化git仓库（如果尚未初始化）
if (-not (Test-Path ".git")) {
    git init
}

# 配置git用户信息
git config user.name "Ink-dark"
git config user.email "209977110+Ink-dark@users.noreply.github.com"

# 切换到main分支
if (-not (git branch --list main)) {
    git checkout -b main
}

# 添加所有文件
git add .

# 提交代码（如果有修改）
if ((git status --porcelain) -ne "") {
    git commit -m "Initial commit"
}

# 添加远程仓库（如果尚未添加）
if (-not (git remote -v | Select-String -Pattern "origin")) {
    git remote add origin https://github.com/Ink-dark/SupeedTOTP.git
}

# 推送代码到GitHub
git push -u origin main
