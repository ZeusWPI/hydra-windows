using Hydra.Models.Info;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.ViewModels.Info {
    public class AcademicCalendarPageViewModel {

        public AcademicPeriod[] Periods { get; set; }

        public Holiday[] Holidays { get; set; }

        public AcademicPeriod[] Vacations { get; set; }

        public AcademicCalendarPageViewModel() {
            Periods = new AcademicPeriod[] {
                new AcademicPeriod() {
                    Start = new DateTime(2015, 9, 21),
                    End = new DateTime(2015, 12, 12),
                    Name = "Lessen eerste semester (12 weken)"
                },
                new AcademicPeriod() {
                    Start = new DateTime(2015, 12, 14),
                    End = new DateTime(2015, 12, 19),
                    Name = "Inhaalweek"
                },
                new AcademicPeriod() {
                    Start = new DateTime(2016, 1, 4),
                    End = new DateTime(2016, 1, 30),
                    Name = "Examens eerste semester"
                },
                new AcademicPeriod() {
                    Start = new DateTime(2016, 2, 8),
                    End = new DateTime(2016, 3, 26),
                    Name = "Lessen tweede semester (7 weken)"
                },
                new AcademicPeriod() {
                    Start = new DateTime(2016, 4, 11),
                    End = new DateTime(2016, 5, 15),
                    Name = "Lessen tweede semester (5 weken)"
                },
                new AcademicPeriod() {
                    Start = new DateTime(2016, 5, 16),
                    End = new DateTime(2016, 5, 21),
                    Name = "Inhaalweek"
                },
                new AcademicPeriod() {
                    Start = new DateTime(2016, 5, 23),
                    End = new DateTime(2016, 7, 2),
                    Name = "Examens tweede semester"
                },
                new AcademicPeriod() {
                    Start = new DateTime(2016, 8, 16),
                    End = new DateTime(2016, 9, 17),
                    Name = "Tweede zit"
                },
                new AcademicPeriod() {
                    Start = new DateTime(2016, 9, 19),
                    End = new DateTime(2016, 9, 24),
                    Name = "Feedbackperiode"
                }
            };

            Holidays = new Holiday[] {
                new Holiday() {
                    Date = new DateTime(2015, 11, 1),
                    Name = "Allerheiligen"
                },
                new Holiday() {
                    Date = new DateTime(2015, 11, 2),
                    Name = "Allerzielen"
                },
                new Holiday() {
                    Date = new DateTime(2015, 11, 11),
                    Name = "Wapenstilstand"
                },
                new Holiday() {
                    Date = new DateTime(2016, 3, 18),
                    Name = "Dies Natalis"
                },
                new Holiday() {
                    Date = new DateTime(2016, 3, 28),
                    Name = "Paasmaandag"
                },
                new Holiday() {
                    Date = new DateTime(2016, 5, 1),
                    Name = "Feest van de arbeid"
                },
                new Holiday() {
                    Date = new DateTime(2016, 5, 5),
                    Name = "O.L.H. Hemelvaart"
                },
                new Holiday() {
                    Date = new DateTime(2016, 5, 6),
                    Name = "Vast opgelegde verlofdag"
                },
                new Holiday() {
                    Date = new DateTime(2016, 5, 16),
                    Name = "Pinkstermaandag"
                },
                new Holiday() {
                    Date = new DateTime(2016, 8, 15),
                    Name = "O.L.V. Hemelvaart"
                }
            };
        }
    }
}
