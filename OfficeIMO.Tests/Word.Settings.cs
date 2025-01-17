﻿using System.IO;
using DocumentFormat.OpenXml.Wordprocessing;
using OfficeIMO.Word;
using Xunit;
using Color = SixLabors.ImageSharp.Color;

namespace OfficeIMO.Tests {
    public partial class Word {
        [Fact]
        public void Test_CreatingDocumentWithSettings() {
            string filePath = Path.Combine(_directoryWithFiles, "CreatingDocumentWithSettings.docx");
            using (WordDocument document = WordDocument.Create(filePath)) {

                document.Settings.ProtectionPassword = "Test";

                Assert.True(document.Settings.ProtectionType == DocumentProtectionValues.ReadOnly);

                Assert.True(document.Settings.Language == "en-US");

                document.Settings.Language = "pl-PL";

                Assert.True(document.Settings.Language == "pl-PL");

                document.Settings.SetBackgroundColor(Color.BlueViolet);

                Assert.True(document.Settings.BackgroundColor == "8A2BE2");

                document.Settings.ZoomPercentage = 150;

                Assert.True(document.Settings.ZoomPercentage == 150);

                Assert.True(document.Settings.UpdateFieldsOnOpen == false);

                document.Settings.UpdateFieldsOnOpen = true;

                Assert.True(document.Settings.UpdateFieldsOnOpen == true);

                document.Save(false);
            }
            using (WordDocument document = WordDocument.Load(Path.Combine(_directoryWithFiles, "CreatingDocumentWithSettings.docx"))) {
                Assert.True(document.Settings.Language == "pl-PL");

                document.Settings.Language = "en-US";

                Assert.True(document.Settings.Language == "en-US");

                Assert.True(document.Settings.ProtectionType == DocumentProtectionValues.ReadOnly);

                document.Settings.RemoveProtection();

                Assert.True(document.Settings.BackgroundColor == "8A2BE2");

                document.Settings.SetBackgroundColor("FFA07A");

                Assert.True(document.Settings.ZoomPercentage == 150);

                document.Settings.ZoomPercentage = 100;

                Assert.True(document.Settings.ZoomPercentage == 100);

                Assert.True(document.Settings.UpdateFieldsOnOpen == true);

                document.Settings.UpdateFieldsOnOpen = false;

                document.Save();
            }
            using (WordDocument document = WordDocument.Load(Path.Combine(_directoryWithFiles, "CreatingDocumentWithSettings.docx"))) {
                Assert.True(document.Settings.ProtectionType == null);
                Assert.True(document.Settings.BackgroundColor == "FFA07A");
                Assert.True(document.Settings.ZoomPercentage == 100);
                Assert.True(document.Settings.UpdateFieldsOnOpen == false);
                document.Save();
            }

            using (WordDocument document = WordDocument.Load(Path.Combine(_directoryWithFiles, "CreatingDocumentWithSettings.docx"))) {

                document.Settings.ZoomPreset = PresetZoomValues.BestFit;

                Assert.True(document.Settings.ZoomPreset == PresetZoomValues.BestFit);

                document.Settings.ZoomPreset = PresetZoomValues.FullPage;

                Assert.True(document.Settings.ZoomPreset == PresetZoomValues.FullPage);

                document.Settings.ZoomPreset = PresetZoomValues.None;

                Assert.True(document.Settings.ZoomPreset == PresetZoomValues.None);

                document.Settings.ZoomPreset = PresetZoomValues.TextFit;

                Assert.True(document.Settings.ZoomPreset == PresetZoomValues.TextFit);

                document.Save();
            }
        }
    }
}
