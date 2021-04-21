using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ConnectionTest.Models;
using NLog;

namespace ConnectionTest
{
    class Program
    {

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            logger.Info("Testing Connection");
            if (!TestConnection()) { logger.Info("No Connection made."); return; }

            InsertRegistrant();
            InsertBallot();
            InsertBallotQuestion("Are you from Michigan?");
            InsertBallotQuestion("Do you like to use insert statements?");
            InsertRegistrantAnswer("N");
            InsertRegistrantAnswer("Y");
           
            logger.Info("Completed Successfully");
        }

        private static void InsertRegistrant()
        {
            int row = 0;
            Int64 AGE = 23;
            //logger.Debug($"Inserting Readings for Equipment");
            try
            {
                string sql = "INSERT INTO Registrant (Firstname, Lastname, Address, City, State, PostCode, PhoneNo, Age, Sex) VALUES (@FN, @LN, @ADD, @City, @ST, @ZIP, @Phone, @Age, @Sex)";
                using SqlConnection conn = GetConnection();
                conn.Open();
                logger.Debug($"Inserting Row for Registrant");
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@FN", "Try"));
                cmd.Parameters.Add(new SqlParameter("@LN", "Test"));
                cmd.Parameters.Add(new SqlParameter("@ADD", "Testing"));
                cmd.Parameters.Add(new SqlParameter("@City", "Work"));
                cmd.Parameters.Add(new SqlParameter("@ST", "OH"));
                cmd.Parameters.Add(new SqlParameter("@ZIP", "43506"));
                cmd.Parameters.Add(new SqlParameter("@Phone", "123456789"));
                cmd.Parameters.Add(new SqlParameter("@Age", AGE));
                cmd.Parameters.Add(new SqlParameter("@Sex", "M"));
                int rowsaffected = cmd.ExecuteNonQuery();
                row = row + rowsaffected;
                logger.Debug($"Rows Affected: {row}");
            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
            }
        }

        private static void InsertBallot()
        {
            int row = 0;
            //logger.Debug($"Inserting Readings for Equipment");
            try
            {
                string sql = "INSERT INTO Ballot (VoterId, VoterDateTime, BallotNumber) VALUES (@VID, @VDT, @BN)";
                using SqlConnection conn = GetConnection();
                conn.Open();
                logger.Debug($"Inserting Row for Ballots");
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@VID", 2));
                cmd.Parameters.Add(new SqlParameter("@VDT", DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("@BN", "B000000002"));
                int rowsaffected = cmd.ExecuteNonQuery();
                row = row + rowsaffected;
                logger.Debug($"Rows Affected: {row}");
            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
            }
        }

        private static void InsertBallotQuestion(String Question)
        {
            int row = 0;
            //logger.Debug($"Inserting Readings for Equipment");
            try
            {
                string sql = "INSERT INTO BallotQuestions (BallotId, Question) VALUES (@BID, @Q)";
                using SqlConnection conn = GetConnection();
                conn.Open();
                logger.Debug($"Inserting Row for Ballot Questions");
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BID", 2));
                cmd.Parameters.Add(new SqlParameter("@Q", $"{Question}"));
                int rowsaffected = cmd.ExecuteNonQuery();
                row = row + rowsaffected;
                logger.Debug($"Rows Affected: {row}");
            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
            }
        }

        private static void InsertRegistrantAnswer(String Answer)
        {
            int row = 0;
            //logger.Debug($"Inserting Readings for Equipment");
            try
            {
                string sql = "INSERT INTO RegistrantAnswer (VoterId, BallotID, Answer) VALUES (@VID, @BID, @Ans)";
                using SqlConnection conn = GetConnection();
                conn.Open();
                logger.Debug($"Inserting Row for Registrant Answers");
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@VID", 2));
                cmd.Parameters.Add(new SqlParameter("@BID", 2));
                cmd.Parameters.Add(new SqlParameter("@Ans", $"{Answer}"));
                int rowsaffected = cmd.ExecuteNonQuery();
                row = row + rowsaffected;
                logger.Debug($"Rows Affected: {row}");
            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
            }
        }

        private static bool TestConnection()
        {
            try
            {
                using SqlConnection conn = GetConnection();
                conn.Open();
                logger.Debug("Connected");
            }
            catch (Exception e)
            {
                logger.Error($"Not Connected {e.StackTrace}");
                return false;
            }
            return true;
        }

        private static SqlConnection GetConnection()
        {
            string _connstr = "Server=LAPTOP-EFG50VT7\\SQLEXPRESS;Database=VoterRegistration;Trusted_Connection=True;MultipleActiveResultSets=true;Connection Timeout=60";
            SqlConnection Connection = null;
            try
            {
                Connection = new SqlConnection(_connstr);
            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
            }
            return Connection;
        }
    }
}
