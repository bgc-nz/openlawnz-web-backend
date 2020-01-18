dotnet restore
dotnet tool install --global dotnet-ef --version 3.1.0
cat << \EOF >> ~/.bash_profile
# Add .NET Core SDK tools
export PATH="$PATH:/home/vscode/.dotnet/tools"
EOF
source ~/.bash_profile
if [ ! -d "Migrations" ]; then
dotnet-ef migrations add InitialCreate
dotnet-ef database update
fi
