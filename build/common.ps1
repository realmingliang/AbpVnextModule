# COMMON PATHS

$rootFolder = (Get-Item -Path "./" -Verbose).FullName

# List of solutions

$solutionPaths = (
    "../modules/account",
	"../modules/identity",
    "../modules/identityServer",
    "../modules/audit-logging",
	"../modules/saas",
	"../modules/setting-management",
	"../sampleapp/aspnet-core"
)