using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentSharp.CoreLib;
using FluentSharp.Git;
using FluentSharp.Git.APIs;
using NUnit.Framework;

namespace TeamMentor.Test
{
    [TestFixture]
    public class Test_Loading_UserData_Repo
    {
        public string Temp_Cloned_Repos    { get; set; }
        public string UserData_Repo_Local  { get; set; }
        //public string UserData_Repo_GitHub { get; set; }

        public Test_Loading_UserData_Repo()
        {
            Temp_Cloned_Repos = "_Temp_Clones".tempDir(false);
            //UserData_Repo_Local  = @"E:\TeamMentor\TM_Releases\TM_Libraries\3.4.1_Userdata\Site_www.teammentor.net";
            //UserData_Repo_Local  = @"E:\TeamMentor\TM_Releases\TM_Libraries\3.4.1_Userdata\Site_www.teammentor.net_Consolidated";
            //UserData_Repo_Local  = @"E:\TeamMentor\TM_Releases\TM_Libraries\3.4.1_Userdata\Site_custX.teammentor.net";
            
            UserData_Repo_Local = "git@github.com:TMClients/Site_www.teammentor.net";       

            Assert.IsNotNull(Temp_Cloned_Repos);
            Assert.IsNotNull(UserData_Repo_Local);
            //Assert.IsNotNull(UserData_Repo_GitHub);

            Assert.IsTrue(Temp_Cloned_Repos.dirExists());
        }
        [Test]
        public void Clone_UserData_for_WWW()
        {
            //Temp_Cloned_Repos.startProcess();
            var repoName = "Site_www";
            var repo_Clone_Name = repoName.append("_NGit_Local_").add_RandomLetters(3);            
            //gitExe_Clone(UserData_Repo_Local, repo_Clone_Name);
            NGit_Clone(UserData_Repo_Local, repo_Clone_Name);
        }

        

        //Util methods (at the moment same code as the one used in Test_Loading_TM_Repos)
        public API_NGit gitExe_Clone(string repo_Source, string repo_Clone_Name)
        {                        
            var repo_Clone  = Temp_Cloned_Repos.pathCombine(repo_Clone_Name);
            var gitCommand = "clone \"{0}\" \"{1}\"".format(repo_Source, repo_Clone_Name);

            Assert.IsFalse(repo_Clone.dirExists());
            
            var start = DateTime.Now;
            gitExe_Execute(gitCommand, Temp_Cloned_Repos);
            "\n\n******* clone in {0} \n\n".debug(start.to_Now_Seconds());

            Assert.IsTrue(repo_Clone.dirExists());
            

            return checkRepo(repo_Source,repo_Clone);
        }
        public string gitExe_Execute(string command, string workDirectory)
        {   
            var GitExe_Path = "git.exe";
            //"[gitExe_Execute] executing command: {0}".debug(command);
            return GitExe_Path.startProcess_getConsoleOut(command,workDirectory).info();
        }
        public API_NGit NGit_Clone(string repo_Source, string repo_Clone_Name)
        {
            var repo_Clone  = Temp_Cloned_Repos.pathCombine(repo_Clone_Name);
            
            Assert.IsFalse  (repo_Clone.dirExists());
            var start       = DateTime.Now;

            var nGit        = repo_Source.git_Clone(repo_Clone);

            "\n\n******* clone in {0} \n\n".debug(start.to_Now_Seconds());
            Assert.IsNotNull(nGit);
            nGit.close();
            return checkRepo(repo_Source,repo_Clone);
        }
         public API_NGit checkRepo(string repo_Source, string repo_Clone)
        {            
            Assert.IsTrue   (repo_Clone.dirExists());

            var start       = DateTime.Now;
            var nGit       = repo_Clone.git_Open();            

            "\n\n******* open in {0} \n\n".debug(start.to_Now_Seconds());
            Assert.IsNotNull(nGit);
            Assert.IsTrue   (repo_Clone.isGitRepository());
            Assert.IsTrue   (nGit.commits_SHA1().notEmpty());
            Assert.IsTrue   (nGit.files().notEmpty());
            Assert.AreEqual (nGit.remote_Url("origin"),repo_Source);            
            return nGit;
        }
    }
}
