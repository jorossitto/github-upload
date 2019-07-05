using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class HouseKeeperServiceTests
    {
        private const string FILENAME = "filename";

        //private Booking _existingBooking;
        //private Mock<IBookingRepository> _repository;
        private HouseKeeperService service;
        private Mock<IStatementGenerator> statementGenerator;
        private Mock<IEmailSender> emailSender;
        private Mock<IXtraMessageBox> messageBox;
        private Mock<IUnitOfWork> unitOfWork;
        private DateTime statementDate = new DateTime(2017, 1, 1);
        private Housekeeper housekeeper;


        [SetUp]
        public void SetUp()
        {
            housekeeper = new Housekeeper
            {
                Email = "a",
                FullName = "b",
                Oid = 1,
                StatementEmailBody = "c"
            };

            unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                housekeeper
            }.AsQueryable());

            statementGenerator = new Mock<IStatementGenerator>();
            emailSender = new Mock<IEmailSender>();
            messageBox = new Mock<IXtraMessageBox>();
            service = new HouseKeeperService
                (
                    unitOfWork.Object,
                    statementGenerator.Object,
                    emailSender.Object,
                    messageBox.Object
                );

        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {

            service.SendStatementEmails(statementDate);
            statementGenerator.Verify(sg => sg.SaveStatement(
                housekeeper.Oid, 
                housekeeper.FullName, 
                (statementDate)));

        }

        [Test]
        public void SendStatementEmails_HouseKeepersEmailIsNull_ShouldNotGenerateStatement()
        {
            housekeeper.Email = null;

            service.SendStatementEmails(statementDate);
            statementGenerator.Verify(sg => sg.SaveStatement(
                housekeeper.Oid,
                housekeeper.FullName,
                (statementDate)), Times.Never);

        }

        [Test]
        public void SendStatementEmails_HouseKeepersEmailIsWhiteSpace_ShouldNotGenerateStatement()
        {
            housekeeper.Email = " ";

            service.SendStatementEmails(statementDate);
            statementGenerator.Verify(sg => sg.SaveStatement(
                housekeeper.Oid,
                housekeeper.FullName,
                (statementDate)), Times.Never);

        }

        [Test]
        public void SendStatementEmails_HouseKeepersEmailIsEmpty_ShouldNotGenerateStatement()
        {
            housekeeper.Email = "";

            service.SendStatementEmails(statementDate);
            statementGenerator.Verify(sg => sg.SaveStatement(
                housekeeper.Oid,
                housekeeper.FullName,
                (statementDate)), Times.Never);

        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            
            statementGenerator.Setup(sg => sg.SaveStatement(
                housekeeper.Oid,
                housekeeper.FullName,
                (statementDate)))
                .Returns(FILENAME);

            service.SendStatementEmails(statementDate);
            emailSender.Verify(es => es.EmailFile(housekeeper.Email, 
                housekeeper.StatementEmailBody, 
                FILENAME,
                It.IsAny<string>()));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_StatementFileInvalid_ShouldNotEmailTheStatement(string email)
        {

            statementGenerator.Setup(sg => sg.SaveStatement(
                housekeeper.Oid,
                housekeeper.FullName,
                (statementDate)))
                .Returns(() => email);

            service.SendStatementEmails(statementDate);

            emailSender.Verify(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()), 
                Times.Never);
        }
    }
}
