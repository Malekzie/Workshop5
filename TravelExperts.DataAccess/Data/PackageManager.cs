using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperts.DataAccess.Models;

namespace TravelExperts.DataAccess.Data
{
    public static class PackageManager
    {

        public static List<Package> GetPackages(TravelExpertsContext db)
        {
            List<Package> packages = db.Packages.Select(p => p).ToList();
            return packages;
        }

        public static List<Package> GetPackageByID(TravelExpertsContext db, int id)
        {
            List<Package> packages = db.Packages.Where(p => p.PackageId == id).ToList();
            return packages;
        }

        public static Package GetPackage(TravelExpertsContext db, int id)
        {
            Package package = db.Packages.FirstOrDefault(p => p.PackageId == id);
            return package;
        }

        public static string GetPackageDesc(Package package, int id)
        {
            return package.PkgDesc;
        }

        public static string GetPackageName(Package package, int id)
        {
            return package.PkgName;
        }
    }
}
