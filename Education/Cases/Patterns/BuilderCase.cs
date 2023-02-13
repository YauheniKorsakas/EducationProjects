using Education.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Education.Cases.Patterns.Builder
{
    public class BuilderCase : ICase
    {
        public async Task RunAsync() {
            var manifestResolver = new ManifestResolver();
            var manifest = manifestResolver.GetManifest();
            manifest.ManifestParts.ForEach(item => Console.WriteLine(item));
        }

        public class ManifestResolver
        {
            private BaseManifestBuilder manifestBuilder = new ManifestBuilder();
            private Manifest manifest;

            public Manifest GetManifest() {
                BuildManifest();
                manifest = manifestBuilder.GetManifest();

                return manifest;
            }

            private void BuildManifest() {
                manifestBuilder.ConstructFirstLevel();
                manifestBuilder.ConstructSecondLevel();

                if (BusinessRulesValidation()) {
                    manifestBuilder.ConstructThirdLevel();
                }
            }

            private bool BusinessRulesValidation() => false;
        }

        public abstract class BaseManifestBuilder
        {
            public abstract void ConstructFirstLevel();
            public abstract void ConstructSecondLevel();
            public abstract void ConstructThirdLevel();
            public abstract Manifest GetManifest();
        }

        public class ManifestBuilder : BaseManifestBuilder
        {
            private readonly Manifest manifest = new Manifest();

            public override void ConstructFirstLevel() {
                manifest.ManifestParts.Add("first level");
            }

            public override void ConstructSecondLevel() {
                manifest.ManifestParts.Add("second level");
            }

            public override void ConstructThirdLevel() {
                manifest.ManifestParts.Add("third level");
            }

            public override Manifest GetManifest() {
                return manifest;
            }
        }

        public class Manifest
        {
            public readonly List<string> ManifestParts = new List<string>();
        }
    }
}
