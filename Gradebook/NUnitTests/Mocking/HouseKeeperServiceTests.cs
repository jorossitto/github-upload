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
        private string statementFileName;

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

            statementFileName = "fileName";
            statementGenerator = new Mock<IStatementGenerator>();
            statementGenerator.Setup(sg => sg.SaveStatement(
                housekeeper.Oid,
                housekeeper.FullName,
                (statementDate)))
                .Returns(() => statementFileName);


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
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_HouseKeepersEmailIsEmpty_ShouldNotGenerateStatement(string email)
        {
            housekeeper.Email = email;

            service.SendStatementEmails(statementDate);
            statementGenerator.Verify(sg => sg.SaveStatement(
                housekeeper.Oid,
                housekeeper.FullName,
                (statementDate)), Times.Never);

        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            service.SendStatementEmails(statementDate);
            VerifyEmailSent();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_StatementFileInvalid_ShouldNotEmailTheStatement(string email)
        {
            statementFileName = email;

            service.SendStatementEmails(statementDate);

            VerifyEmailNotSent();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_EmailSendingFails_DisplayAMessageBox(string email)
        {
            emailSender.Setup(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()
                )).Throws<Exception>();

            service.SendStatementEmails(statementDate);

            messageBox.Verify(mb => mb.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }

        private void VerifyEmailNotSent()
        {
            emailSender.Verify(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
                Times.Never);
        }
        private void VerifyEmailSent()
        {
            emailSender.Verify(es => es.EmailFile(housekeeper.Email,
                housekeeper.StatementEmailBody,
                statementFileName,
                It.IsAny<string>()));
        }
    }
}
