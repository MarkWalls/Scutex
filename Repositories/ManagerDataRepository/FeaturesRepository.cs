﻿using System.Linq;
using AutoMapper;
using WaveTech.Scutex.Model.Interfaces.Repositories;

namespace WaveTech.Scutex.Repositories.ManagerDataRepository
{
	internal class FeaturesRepository : IFeaturesRepository
	{
		private readonly ScutexEntities db;

		public FeaturesRepository(ScutexEntities db)
		{
			this.db = db;
		}

		public IQueryable<Model.Feature> GetAllFeatures()
		{
			return from feature in db.Features
						 select new Model.Feature
						 {
							 ProductFeatureId = feature.FeatureId,
							 ProductId = feature.Product.ProductId,
							 Name = feature.Name,
							 UniquePad = feature.UniquePad
						 };
		}

		public IQueryable<Model.Feature> GetFeatureById(int featureId)
		{
			return from feature in GetAllFeatures()
						 where feature.ProductFeatureId == featureId
						 select feature;
		}

		public IQueryable<Model.Feature> InsertFeature(Model.Feature feature)
		{
			int newId;

			//using (ScutexEntities db1 = new ScutexEntities())
			//{
			Feature feat = new Feature();

			Mapper.CreateMap<Model.Feature, Feature>();
			feat = Mapper.Map<Model.Feature, Feature>(feature);

			db.AddToFeatures(feat);
			db.SaveChanges();

			newId = feat.FeatureId;
			//}

			return GetFeatureById(newId);
		}

		public IQueryable<Model.Feature> UpdateFeature(Model.Feature feature)
		{
			int newId;

			//using (ScutexEntities db1 = new ScutexEntities())
			//{
			var feat = (from f in db.Features
									where f.FeatureId == feature.ProductFeatureId
									select f).First();

			feat.Name = feature.Name;
			feat.ProductId = feature.ProductId;
			feat.UniquePad = feature.UniquePad;


			db.SaveChanges();

			newId = feat.FeatureId;
			//}

			return GetFeatureById(newId);
		}
	}
}
