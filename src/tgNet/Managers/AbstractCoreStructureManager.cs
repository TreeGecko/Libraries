using MongoDB.Driver;
using TreeGecko.Library.Mongo.Managers;
using TreeGecko.Library.Net.DAOs;

namespace TreeGecko.Library.Net.Managers
{
    public abstract class AbstractCoreStructureManager : AbstractMongoManager
    {
        public abstract void BuildDB();

        protected AbstractCoreStructureManager(MongoDatabase _mongoDB) 
            : base(_mongoDB)
        {
        }

        protected AbstractCoreStructureManager(string _name) 
            : base(_name)
        {
            
        }

        public void BuildDB(bool _useTGUserDAO)
        {
            CannedEmailDAO cannedEmailDAO = new CannedEmailDAO(MongoDB);
            cannedEmailDAO.BuildTable();

            SystemEmailDAO systemEmailDAO = new SystemEmailDAO(MongoDB);
            systemEmailDAO.BuildTable();

            TGEmailDAO emailDAO = new TGEmailDAO(MongoDB);
            emailDAO.BuildTable();

            TGEulaAgreementDAO eulaAgreementDAO = new TGEulaAgreementDAO(MongoDB);
            eulaAgreementDAO.BuildTable();

            TGEulaDAO eulaDAO = new TGEulaDAO(MongoDB);
            eulaDAO.BuildTable();

            TGUserAuthorizationDAO userAuthorizationDAO = new TGUserAuthorizationDAO(MongoDB);
            userAuthorizationDAO.BuildTable();

            if (_useTGUserDAO)
            {
                TGUserDAO userDAO = new TGUserDAO(MongoDB);
                userDAO.BuildTable();
            }

            TGUserEmailValidationDAO userEmailValidationDAO = new TGUserEmailValidationDAO(MongoDB);
            userEmailValidationDAO.BuildTable();

            TGUserPasswordDAO userPasswordDAO = new TGUserPasswordDAO(MongoDB);
            userPasswordDAO.BuildTable();

            TGUserRoleDAO userRoleDAO = new TGUserRoleDAO(MongoDB);
            userRoleDAO.BuildTable();

            WebLogEntryDAO webLogEntryDAO = new WebLogEntryDAO(MongoDB);
            webLogEntryDAO.BuildTable();
        }
    }
}
