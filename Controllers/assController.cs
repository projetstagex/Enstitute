// using System;
// using System.Diagnostics;
// using System.Linq;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using Enstitute.Models;
// using Microsoft.AspNetCore.Http;

// namespace Enstitute.Controllers
// {

//     public class assController : Controller
//     {
//         private readonly ILogger<assController> _logger;

//         public assController(ILogger<assController> logger)
//         {
//             _logger = logger;
//         }
//         public IActionResult Index(){

//             var db = new ApplicationContext();
            
//             var gender1 = new Gender("Homme", 1);
//             var gender2 = new Gender("Femme", 2);
//             var gender3 = new Gender("Autre", 3);
            
//             db.Add(gender1);
//             db.Add(gender2);
//             db.Add(gender3);

//             var address1 = new Address("streetAddress1", "appartementAddress1", "2332", "marrakech");
            
//             db.Add(address1);

//             var scholaryear1 = new ScholarYear(2019,2020);
//             var scholaryear2 = new ScholarYear(2020,2021);
            
//             db.Add(scholaryear1);
//             db.Add(scholaryear2);

//             var day1 = new Day("Lundi", 1);
//             var day2 = new Day("Mardi", 2);
//             var day3 = new Day("Mercredi", 3);
//             var day4 = new Day("Jeudi", 4);
//             var day5 = new Day("Vendredi", 5);
//             var day6 = new Day("Samedi", 6);
            
//             db.Add(day1);
//             db.Add(day2);
//             db.Add(day3);
//             db.Add(day4);
//             db.Add(day5);
//             db.Add(day6);


//             var departement1 = new Departement("Computer Science");
//             var departement0 = new Departement("Physics");
            
//             db.Add(departement1);
//             db.Add(departement0);

//             var formation0 = new Formation(2, "Thermodynamique", "THD", departement0);
//             var formation1 = new Formation(3, "thecnicien developement informatique", "TDI", departement1);
//             var formation2 = new Formation(2, "thecnicien Reseaux informatique", "TRI", departement1);
            
//             db.Add(formation1);
//             db.Add(formation2);
//             db.Add(formation0);

//             var grade1 = new Grade("1ere année", "1");
//             var grade2 = new Grade("2eme année", "2");
//             var grade3 = new Grade("3ele année", "3");
            
//             db.Add(grade1);
//             db.Add(grade2);
//             db.Add(grade3);

//             var sector1 = new Sector("TDI-1", "1", grade1, formation1);
//             var sector2 = new Sector("TDI-2", "2", grade2, formation1);
//             var sector3 = new Sector("TDI-3", "3", grade3, formation1);
//             var sector4 = new Sector("TRI-1", "4", grade1, formation2);
//             var sector5 = new Sector("TRI-2", "5", grade2, formation2);
//             var sector6 = new Sector("TRI-3", "6", grade3, formation2);
            
//             db.Add(sector1);
//             db.Add(sector2);
//             db.Add(sector3);
//             db.Add(sector4);
//             db.Add(sector5);
//             db.Add(sector6);

//             var group1 = new Group("TDI-1-A-1", sector1, "TDI1A1", scholaryear1);
//             var group2 = new Group("TDI-1-B-1", sector1, "TDI1B1", scholaryear1);
//             var group3 = new Group("TDI-2-A-1", sector2, "TDI2A1", scholaryear1);
//             var group4 = new Group("TDI-2-B-1", sector2, "TDI2B1", scholaryear1);
//             var group5 = new Group("TDI-3-A-1", sector3, "TDI3A1", scholaryear1);
//             var group6 = new Group("TDI-3-B-1", sector3, "TDI3B1", scholaryear1);
//             var group7 = new Group("TRI-1-A-1", sector4, "TRI1A1", scholaryear1);
//             var group8 = new Group("TRI-1-B-1", sector4, "TRI1B1", scholaryear1);
//             var group9 = new Group("TRI-2-A-1", sector5, "TRI2A1", scholaryear1);
//             var group10 = new Group("TRI-2-B-1", sector5, "TRI2B1", scholaryear1);
//             var group11 = new Group("TRI-3-A-1", sector6, "TRI3A1", scholaryear1);
//             var group12 = new Group("TRI-3-B-1", sector6, "TRI3B1", scholaryear1);
            
//             db.Add(group1);
//             db.Add(group2);
//             db.Add(group3);
//             db.Add(group4);
//             db.Add(group5);
//             db.Add(group6);
//             db.Add(group7);
//             db.Add(group8);
//             db.Add(group9);
//             db.Add(group10);
//             db.Add(group11);
//             db.Add(group12);

//             var module1 = new Module(2, "Conception orienté object", "UML"); 
//             var module2 = new Module(2, "Oriented object Programming", "OOP"); 
//             var module3 = new Module(2, "TCP/IP", "TCP"); 
//             var module4 = new Module(3, "Cloud Computing", "CC"); 

//             db.Add(module1);
//             db.Add(module2);
//             db.Add(module3);
//             db.Add(module4);

//             var precision1 = new Precision("Diagramme de class","UML1", module1);
//             var precision2 = new Precision("Diagramme de Sequences","UML2", module1);
//             var precision3 = new Precision("Qu'est ce qu'une classe","OOP1", module2);
//             var precision4 = new Precision("Notion d'une objet","OOP2", module2);
//             var precision5 = new Precision("Modele OSI","TCP1", module3);
//             var precision6 = new Precision("Routage","TCP2", module3);
//             var precision7 = new Precision("IAAS","CC1", module4);
//             var precision8 = new Precision("SAAS","CC2", module4);

//             db.Add(precision1);
//             db.Add(precision2);
//             db.Add(precision3);
//             db.Add(precision4);
//             db.Add(precision5);
//             db.Add(precision6);
//             db.Add(precision7);
//             db.Add(precision8);

//             var context1 = new Context("Entités", precision1, "UML11");
//             var context2 = new Context("Liaison", precision1, "UML12");
//             var context3 = new Context("Acteurs", precision2, "UML21");
//             var context4 = new Context("Cadres d'interaction", precision2, "UML22");
//             var context5 = new Context("attributs", precision3, "OOP11");
//             var context6 = new Context("methodes", precision3, "OOP12");
//             var context7 = new Context("Constructeurs", precision4, "OOP21");
//             var context8 = new Context("Destructeurs", precision4, "OOP22");
//             var context9 = new Context("couche physique", precision5, "TCP11");
//             var context10 = new Context("couche liaison", precision5, "TCP12");
//             var context11 = new Context("Le protocole IP", precision6, "TCP21");
//             var context12 = new Context("Le protocole IP-2", precision6, "TCP21");
//             var context13 = new Context("Logs and Metrics", precision7, "CC11");
//             var context14 = new Context("Azure Virtual Machines (VMs)", precision7, "CC12");
//             var context15 = new Context("SAAS1", precision8, "CC21");
//             var context16 = new Context("SAAS2", precision8, "CC22");

//             db.Add(context1);
//             db.Add(context2);
//             db.Add(context3);
//             db.Add(context4);
//             db.Add(context5);
//             db.Add(context6);
//             db.Add(context7);
//             db.Add(context8);
//             db.Add(context9);
//             db.Add(context10);
//             db.Add(context11);
//             db.Add(context12);
//             db.Add(context13);
//             db.Add(context14);
//             db.Add(context15);
//             db.Add(context16);


//             var email = "email@email.com";var phone = "060000010";var NIC = "nic_former";var id = "id_former";var rn = "rn_former";
//             var former1  = new Former("former1", "password1", "fn1", "ln1", 1991, 10, 19, gender1, address1, phone, email, NIC+"1", id+"1", rn+"1" );
//             var former2  = new Former("former2", "password2", "fn2", "ln2", 1985, 10, 19, gender2, address1, phone, email, NIC+"2", id+"2", rn+"2" );
//             var former3  = new Former("former3", "password3", "fn3", "ln3", 1989, 10, 19, gender1, address1, phone, email, NIC+"3", id+"3", rn+"3" );
//             var former4  = new Former("former4", "password4", "fn4", "ln4", 1990, 10, 19, gender2, address1, phone, email, NIC+"4", id+"4", rn+"4" );
//             db.Add(former1);
//             db.Add(former2);
//             db.Add(former3);
//             db.Add(former4);

//             var schedule1 = new Schedule(former1, scholaryear1);
//             var schedule2 = new Schedule(former1, scholaryear2);
//             var schedule3 = new Schedule(former2, scholaryear1);
//             var schedule4 = new Schedule(former2, scholaryear2);
//             var schedule5 = new Schedule(former3, scholaryear1);
//             var schedule6 = new Schedule(former3, scholaryear2);
//             var schedule7 = new Schedule(former4, scholaryear1);
//             var schedule8 = new Schedule(former4, scholaryear2);

//             db.Add(schedule1);
//             db.Add(schedule2);
//             db.Add(schedule3);
//             db.Add(schedule4);
//             db.Add(schedule5);
//             db.Add(schedule6);
//             db.Add(schedule7);
//             db.Add(schedule8);

//             var id_staff = "id_staff";
//             var rn_staff = "rn_staff";

//             var director    = new Director ("director", "passworddirector", "fn5", "ln5",   1980, 10, 19, gender2, address1, phone, email, "WXCVB1", id_staff+"1", rn_staff+"1" );
//             var secretary   = new Secretary("secretary", "passwordsecretary", "fn6", "ln6", 1999, 10, 19, gender2, address1, phone, email, "WXCVB2", id_staff+"2", rn_staff+"2" );
//             var staff       = new Staff    ("admin", "admin", "hamza", "sabib",             1999, 10, 19, gender1, address1, phone, email, "XAXAXA", id_staff+"3" );
            
//             db.Add(director);
//             db.Add(secretary);
//             db.Add(staff);

//             var ma1 = new ModuleAffectation(group1, former1, module1);
//             var ma2 = new ModuleAffectation(group2, former1, module1);
//             var ma3 = new ModuleAffectation(group3, former1, module1);
//             var ma4 = new ModuleAffectation(group4, former1, module1);
//             var ma5 = new ModuleAffectation(group5, former1, module1);
//             var ma6 = new ModuleAffectation(group6, former1, module1);
//             var ma7 = new ModuleAffectation(group1, former2, module2);
//             var ma8 = new ModuleAffectation(group2, former2, module2);
//             var ma9 = new ModuleAffectation(group3, former2, module2);
//             var ma10 = new ModuleAffectation(group4, former2, module2);
//             var ma11 = new ModuleAffectation(group5, former2, module2);
//             var ma12 = new ModuleAffectation(group6, former2, module2);
//             var ma13 = new ModuleAffectation(group7, former3, module3);
//             var ma14 = new ModuleAffectation(group8, former3, module3);
//             var ma15 = new ModuleAffectation(group9, former3, module3);
//             var ma16 = new ModuleAffectation(group10, former3, module3);
//             var ma17 = new ModuleAffectation(group11, former3, module3);
//             var ma18 = new ModuleAffectation(group12, former3, module3);
//             var ma19 = new ModuleAffectation(group7, former4, module4);
//             var ma20 = new ModuleAffectation(group8, former4, module4);
//             var ma21 = new ModuleAffectation(group9, former4, module4);
//             var ma22 = new ModuleAffectation(group10, former4, module4);
//             var ma23 = new ModuleAffectation(group11, former4, module4);
//             var ma24 = new ModuleAffectation(group12, former4, module4);

//             db.Add(ma1);
//             db.Add(ma2);
//             db.Add(ma3);
//             db.Add(ma4);
//             db.Add(ma5);
//             db.Add(ma6);
//             db.Add(ma7);
//             db.Add(ma8);
//             db.Add(ma9);
//             db.Add(ma10);
//             db.Add(ma11);
//             db.Add(ma12);
//             db.Add(ma13);
//             db.Add(ma14);
//             db.Add(ma15);
//             db.Add(ma16);
//             db.Add(ma17);
//             db.Add(ma18);
//             db.Add(ma19);
//             db.Add(ma20);
//             db.Add(ma21);
//             db.Add(ma22);
//             db.Add(ma23);
//             db.Add(ma24);
            

//             var session1 = new Session(group1, day1, schedule1, 8, 30, 12, 30);
//             var session2 = new Session(group1, day2, schedule3, 8, 30, 12, 30);
//             var session3 = new Session(group2, day1, schedule1, 8, 30, 12, 30);
//             var session4 = new Session(group2, day2, schedule3, 8, 30, 12, 30);
//             var session5 = new Session(group3, day1, schedule1, 8, 30, 12, 30);
//             var session6 = new Session(group3, day2, schedule3, 8, 30, 12, 30);
//             var session7 = new Session(group4, day1, schedule1, 8, 30, 12, 30);
//             var session8 = new Session(group4, day2, schedule3, 8, 30, 12, 30);
//             var session9 = new Session(group5, day1, schedule1, 8, 30, 12, 30);
//             var session10 = new Session(group5, day2, schedule3, 8, 30, 12, 30);
//             var session11 = new Session(group6, day1, schedule1, 8, 30, 12, 30);
//             var session12 = new Session(group6, day2, schedule3, 8, 30, 12, 30);
//             var session13 = new Session(group7, day1, schedule5, 8, 30, 12, 30);
//             var session14 = new Session(group7, day2, schedule7, 8, 30, 12, 30);
//             var session15 = new Session(group8, day1, schedule5, 8, 30, 12, 30);
//             var session16 = new Session(group8, day2, schedule7, 8, 30, 12, 30);
//             var session17 = new Session(group9, day1, schedule5, 8, 30, 12, 30);
//             var session18 = new Session(group9, day2, schedule7, 8, 30, 12, 30);
//             var session19 = new Session(group10, day1, schedule5, 8, 30, 12, 30);
//             var session20 = new Session(group10, day2, schedule7, 8, 30, 12, 30);
//             var session21 = new Session(group11, day1, schedule5, 8, 30, 12, 30);
//             var session22 = new Session(group11, day2, schedule7, 8, 30, 12, 30);
//             var session23 = new Session(group12, day1, schedule5, 8, 30, 12, 30);
//             var session24 = new Session(group12, day2, schedule7, 8, 30, 12, 30);

//             db.Add(session1);
//             db.Add(session2);
//             db.Add(session3);
//             db.Add(session4);
//             db.Add(session5);
//             db.Add(session6);
//             db.Add(session7);
//             db.Add(session8);
//             db.Add(session9);
//             db.Add(session10);
//             db.Add(session11);
//             db.Add(session12);
//             db.Add(session13);
//             db.Add(session14);
//             db.Add(session15);
//             db.Add(session16);
//             db.Add(session17);
//             db.Add(session18);
//             db.Add(session19);
//             db.Add(session20);
//             db.Add(session21);
//             db.Add(session22);
//             db.Add(session23);
//             db.Add(session24);


//             var id_student = "id_student";
//             var nic_student="nic_student";
//             var in_student = "in_student";
//             var natio = "morrocan";

//             var student1  = new Student("student1", "passwords1",    "fns1", "lns1",  2000, 10, 10, gender1, address1, phone , email, nic_student+ "1",  group1,  natio, id_student+  "1", in_student+ "1");
//             var student2  = new Student("student2", "passwords2",    "fns2", "lns2",  2000, 10, 10, gender2, address1, phone , email, nic_student+ "2",  group1,  natio, id_student+  "2", in_student+ "2");
//             var student3  = new Student("student3", "passwords3",    "fns3", "lns3",  2000, 10, 10, gender1, address1, phone , email, nic_student+ "3",  group2,  natio, id_student+  "3", in_student+ "3");
//             var student4  = new Student("student4", "passwords4",    "fns4", "lns4",  2000, 10, 10, gender1, address1, phone , email, nic_student+ "4",  group2,  natio, id_student+  "4", in_student+ "4");
//             var student5  = new Student("student5", "passwords5",    "fns5", "lns5",  2000, 10, 10, gender1, address1, phone , email, nic_student+ "5",  group3,  natio, id_student+  "5", in_student+ "5");
//             var student6  = new Student("student6", "passwords6",    "fns6", "lns6",  2000, 10, 10, gender1, address1, phone , email, nic_student+ "6",  group3,  natio, id_student+  "6", in_student+ "6");
//             var student7  = new Student("student7", "passwords7",    "fns7", "lns7",  2000, 10, 10, gender1, address1, phone , email, nic_student+ "7",  group4,  natio, id_student+  "7", in_student+ "7");
//             var student8  = new Student("student8", "passwords8",    "fns8", "lns8",  2000, 10, 10, gender1, address1, phone , email, nic_student+ "8",  group4,  natio, id_student+  "8", in_student+ "8");
//             var student9  = new Student("student9", "passwords9",    "fns9", "lns9",  2000, 10, 10, gender1, address1, phone , email, nic_student+ "9",  group5,  natio, id_student+  "9", in_student+ "9");
//             var student10 = new Student("student10", "passwords10", "fns10", "lns10", 2000, 10, 10, gender1, address1, phone , email, nic_student+ "10", group5,  natio, id_student+ "10", in_student+ "10");
//             var student11 = new Student("student11", "passwords11", "fns11", "lns11", 2000, 10, 10, gender1, address1, phone , email, nic_student+ "11", group6,  natio, id_student+ "11", in_student+ "11");
//             var student12 = new Student("student12", "passwords12", "fns12", "lns12", 2000, 10, 10, gender1, address1, phone , email, nic_student+ "12", group6,  natio, id_student+ "12", in_student+ "12");
//             var student13 = new Student("student13", "passwords13", "fns13", "lns13", 2000, 10, 10, gender1, address1, phone , email, nic_student+ "13", group7,  natio, id_student+ "13", in_student+ "13");
//             var student14 = new Student("student14", "passwords14", "fns14", "lns14", 2000, 10, 10, gender1, address1, phone , email, nic_student+ "14", group7,  natio, id_student+ "14", in_student+ "14");
//             var student15 = new Student("student15", "passwords15", "fns15", "lns15", 2000, 10, 10, gender1, address1, phone , email, nic_student+ "15", group8,  natio, id_student+ "15", in_student+ "15");
//             var student16 = new Student("student16", "passwords16", "fns16", "lns16", 2000, 10, 10, gender1, address1, phone , email, nic_student+ "16", group8,  natio, id_student+ "16", in_student+ "16");
//             var student17 = new Student("student17", "passwords17", "fns17", "lns17", 2000, 10, 10, gender1, address1, phone , email, nic_student+ "17", group9,  natio, id_student+ "17", in_student+ "17");
//             var student18 = new Student("student18", "passwords18", "fns18", "lns18", 2000, 10, 10, gender1, address1, phone , email, nic_student+ "18", group9,  natio, id_student+ "18", in_student+ "18");
//             var student19 = new Student("student19", "passwords19", "fns19", "lns19", 2000, 10, 10, gender1, address1, phone , email, nic_student+ "19", group10, natio, id_student+ "19", in_student+ "19");
//             var student20 = new Student("student20", "passwords20", "fns20", "lns20", 2000, 10, 10, gender1, address1, phone , email, nic_student+ "20", group10, natio, id_student+ "20", in_student+ "20");
//             var student21 = new Student("student21", "passwords21", "fns21", "lns21", 2000, 10, 10, gender1, address1, phone , email, nic_student+ "21", group11, natio, id_student+ "21", in_student+ "21");
//             var student22 = new Student("student22", "passwords22", "fns22", "lns22", 2000, 10, 10, gender1, address1, phone , email, nic_student+ "22", group11, natio, id_student+ "22", in_student+ "22");
//             var student23 = new Student("student23", "passwords23", "fns23", "lns23", 2000, 10, 10, gender1, address1, phone , email, nic_student+ "23", group12, natio, id_student+ "23", in_student+ "23");
//             var student24 = new Student("student24", "passwords24", "fns24", "lns24", 2000, 10, 10, gender1, address1, phone , email, nic_student+ "24", group12, natio, id_student+ "24", in_student+ "24");
            
//             db.Add(student1);
//             db.Add(student2);
//             db.Add(student3);
//             db.Add(student4);
//             db.Add(student5);
//             db.Add(student6);
//             db.Add(student7);
//             db.Add(student8);
//             db.Add(student9);
//             db.Add(student10);
//             db.Add(student11);
//             db.Add(student12);
//             db.Add(student13);
//             db.Add(student14);
//             db.Add(student15);
//             db.Add(student16);
//             db.Add(student17);
//             db.Add(student18);
//             db.Add(student19);
//             db.Add(student20);
//             db.Add(student21);
//             db.Add(student22);
//             db.Add(student23);
//             db.Add(student24);

//             var absence1 = new Absence(session1, student1, DateTime.Now.Date, true);
//             var absence2 = new Absence(session1, student2, DateTime.Now.Date, true);
//             var absence3 = new Absence(session2, student1, DateTime.Now.Date, false);
//             var absence4 = new Absence(session2, student2, DateTime.Now.Date, false);

//             db.Add(absence1);
//             db.Add(absence2);
//             db.Add(absence3);
//             db.Add(absence4);
            

//             db.SaveChanges();

            
//             return View();
//         }

  

//         [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//         public IActionResult Error()
//         {
//             return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//         }
//     }
// }
