using Nuke.Common;

class Build : NukeBuild, IDefaultBuild
{
    public static int Main () => Execute<Build>(x => ((IDefaultBuild)x).Formatting);
}
