using System;
using System.Collections;
using System.Diagnostics;
using System.Text;

namespace LeastSignificantBit
{
    public partial class Form1 : Form
    {
        const string workingDirectory = "C:\\Users\\Vratislav\\Pictures\\";
        bool debug = true;

        bool encoding = true;
        string originalImagePath = "";

        Bitmap originalImage;
        Bitmap addjustedImage;

        long availableMessageSize;
        long currentMessageSize;

        int requestedBitsToEncodeSize;
        int usedBits;

        int[] powers;

        string algorithm = "lsb";

        public Form1() {
            InitializeComponent();
            InitPowersOf2(32);
            label7.Text = "Celková dostupná velikost zprávy: 0B\nSkuteèná dostupná velikost zprávy: 0B";
            ChangeAlgorithm();
        }

        bool IsExpertiment => experimentToggle.Checked;

        bool TryToTrimBorders => checkBox1.Checked;


        private void StartEncoding() {
            if (algorithm == "lsb") {
                EncodeUsingLsb();
            } else if (algorithm == "pvd") {
                EncodeUsingPvd();
            }
        }

        private void StartDecoding() {
            if (algorithm == "lsb") {
                DecodeUsingLsb();
            } else if (algorithm == "pvd") {
                DecodeUsingPvd();
            }
            MessageBox.Show("Hotovo :)");
        }

        #region Help methods

        private string GetByComp(int comp) {
            if (comp == 0) return "R";
            if (comp == 1) return "G";
            if (comp == 2) return "B";
            return "-";
        }

        private void DebugRemainingData(BitArray messageInBits, int startIndex) {
            string toDebug = "";
            for (int i = startIndex; i < messageInBits.Length; i++)
                toDebug += messageInBits[i] ? "1" : "0";
            Debug.WriteLine("Remains to write: " + toDebug);
        }

        private void DebugData(byte bt) {
            string a = Convert.ToString(bt, 2).PadLeft(8, '0');
            Debug.WriteLineIf(bt != 0, bt + " - " + a);
        }

        private void InitPowersOf2(int bits) {
            powers = new int[bits];
            powers[0] = 1;
            for (int i = 1; i < bits; i++) {
                powers[i] = powers[i - 1] * 2;
            }

            powers = powers.Reverse().ToArray();
        }

        private int GetMaxDiffForPixel(int bits) {
            int sum = 0;
            for (int i = 0; i < bits; i++)
                sum += (int) (Math.Pow(2, i));
            return sum;
        }

        private int ComparePixel(Color orig, Color changed) {
            int diff = 0;
            diff += Math.Abs(orig.R - changed.R);
            diff += Math.Abs(orig.G - changed.G);
            diff += Math.Abs(orig.B - changed.B);

            return diff;
        }

        private bool ComparePixelDetailed(Color orig, Color changed, out string text) {
            if (ComparePixel(orig, changed) > 0) {
                text = $"R({Math.Abs(orig.R - changed.R)}) G({Math.Abs(orig.G - changed.G)}) B({Math.Abs(orig.B - changed.B)})";
                return true;
            }
            text = "";
            return false;
        }

        int BitsToFillFullColor(int req) {
            int mod = req % (usedBits * 3);
            if (mod == 0)
                return req / 3;
            return req + (usedBits * 3) - mod;
        }

        private bool[] ConvertStringToBoolArray(string str) {
            str = str.PadLeft(8, '0');
            bool[] b = new bool[8];
            for (int i = 0; i < str.Length; i++) {
                b[i] = str[i] == '1';
            }
            return b;
        }

        private bool[] ConvertStringBinaryToBoolArray(string str) {
            bool[] b = new bool[str.Length];
            for (int i = 0; i < str.Length; i++) {
                b[i] = str[i] == '1';
            }
            return b;
        }

        private BitArray ReverseBitArray(BitArray array) {
            BitArray ba = new BitArray(array.Count);

            for (int i = 0; i < array.Count; i++) {
                ba[i] = array[array.Count - i - 1];
            }

            return ba;
        }

        private Color CreateColorFromBits(bool[][] tempBitArray) {

            return Color.FromArgb(
                GetIntFromBitArray(tempBitArray[0]),
                GetIntFromBitArray(tempBitArray[1]),
                GetIntFromBitArray(tempBitArray[2])
                );
        }

        private int GetIntFromBitArray(bool[] bitArray) {

            if (bitArray.Length > 32)
                throw new ArgumentException("Argument length shall be at most 32 bits.");

            int r = 0;
            int powIndex = powers.Length - 1;
            for (int i = bitArray.Length - 1; i >= 0; i--) {
                if (bitArray[i]) r += powers[powIndex];
                powIndex--;
            }
            return r;
        }

        private byte GetColorData(Color c, int index) {
            switch (index) {
                case 0: return c.R;
                case 1: return c.G;
                case 2: return c.B;
            }
            return 0;
        }

        public string Reverse(string s) {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        #endregion

        #region Function methods

        #region Least significant bit
        private void EncodeUsingLsb() {
            string message = richTextBox1.Text;
            var bytesToSave = Encoding.ASCII.GetBytes(message);
            var messageInBits = new BitArray(bytesToSave); // velikost zprávy pøevedené na bity

            usedBits = (int) numericUpDown1.Value;

            EncodeSize(messageInBits);
            EncodeData(messageInBits);

            TryToSaveResultImage();

        }

        private void DecodeUsingLsb() {
            int chars = DecodeSize();
            string data = DecodeData(chars);
            richTextBox1.Text = data;
        }

        private void CalculateAvailableSize() {
            if (originalImage == null)
                return;
            int resolution = originalImage.Width * originalImage.Height;
            int sumSize = resolution * 3 * (int) numericUpDown1.Value;
            requestedBitsToEncodeSize = (int) Math.Ceiling(Math.Log2(sumSize));
            int realSize = sumSize - requestedBitsToEncodeSize;

            availableMessageSize = realSize;


            label7.Text = $"Celková dostupná velikost zprávy: {UnitConverter.SizeSuffix(sumSize)}\nSkuteèná dostupná velikost zprávy: {UnitConverter.SizeSuffix(realSize)}";
        }

        private void CalculateRequestedSize() {
            string message = richTextBox1.Text;
            var bytes = Encoding.ASCII.GetBytes(message);
            currentMessageSize = bytes.LongLength;
            var length = UnitConverter.SizeSuffix(currentMessageSize);

            // Zobrazení
            label5.Text = $"Velikost zprávy (ASII): {length}";
        }

        private void EncodeSize(BitArray messageInBits) {

            var messageLength = messageInBits.Length;
            Debug.WriteLine("Poèet bitù pro uložení zprávy: " + messageLength);
            BitArray sizeInBits = new BitArray(new int[] { messageLength });

            int bitsToFillFullColor = BitsToFillFullColor(requestedBitsToEncodeSize);
            Debug.WriteLine($"Pro uložení maximální velikosti zprávy bude potøeba {requestedBitsToEncodeSize}b");
            Debug.WriteLine($"Což by se mìlo pøi bitcountu {usedBits} vejít do {bitsToFillFullColor / (3 * usedBits)} pixelù");

            var allBitsToSave = new BitArray(bitsToFillFullColor);
            //int maxChangeForPixel = GetMaxDiffForPixel(usedBits);
            //Debug.WriteLine("Maximální hodnota zmìny pro pixel je " + maxChangeForPixel);

            int startIndex = -1;
            if (BitConverter.IsLittleEndian) {
                sizeInBits = ReverseBitArray(sizeInBits);
            }

            for (int i = 0; i < sizeInBits.Length; i++) {
                if (sizeInBits[i] == true) {
                    startIndex = i;
                    break;
                }
            }


            if (startIndex == -1) { // to by se stát nemìlo
                //MessageBox.Show("Nìco je špatnì s velikostí zprávy...");
                return;
            }

            for (int i = 0; i < sizeInBits.Length - startIndex; i++) {
                allBitsToSave[allBitsToSave.Length - i - 1] = sizeInBits[sizeInBits.Length - i - 1];
            }

            int bitIndex = 0;
            int bitOffsetInByte = 8 - usedBits;

            bool[][] tempBitArray = new bool[3][];

            for (int i = 0; i < originalImage.Width; i++) {
                if (bitIndex == allBitsToSave.Count)
                    break;
                for (int j = 0; j < originalImage.Height; j++) {

                    if (bitIndex == allBitsToSave.Count)
                        break;

                    Color pixel = originalImage.GetPixel(i, j);

                    for (int col = 0; col < 3; col++) {

                        byte a = GetColorData(pixel, col);
                        bool[] b = ConvertStringToBoolArray(Convert.ToString(a, 2));

                        tempBitArray[col] = b;

                        if (bitIndex < allBitsToSave.Count) {
                            for (int r = bitOffsetInByte; r < 8; r++) {
                                tempBitArray[col][r] = allBitsToSave[bitIndex++];
                            }
                        }
                    }


                    Color newPixel = CreateColorFromBits(tempBitArray);

                    addjustedImage.SetPixel(i, j, newPixel);
                }
            }
        }

        private void EncodeData(BitArray messageInBits) {

            int pixelSizeOffset = BitsToFillFullColor(requestedBitsToEncodeSize);
            if (BitConverter.IsLittleEndian) {
                messageInBits = ReverseBitArray(messageInBits);
            }

            int bitIndex = 0;
            int bitOffsetInByte = 8 - usedBits;

            bool[][] tempBitArray = new bool[3][];

            for (int i = 0; i < originalImage.Width; i++) {

                if (bitIndex == messageInBits.Count && !IsExpertiment)
                    break;
                for (int j = 0; j < originalImage.Height; j++) {

                    if (i * Width + j < pixelSizeOffset) {
                        continue;
                    } // skip size pixels

                    if (bitIndex == messageInBits.Count && !IsExpertiment)
                        break;

                    Color pixel = originalImage.GetPixel(i, j);

                    for (int col = 0; col < 3; col++) {

                        if (IsExpertiment) {
                            byte a = GetColorData(pixel, col);
                            bool[] b = ConvertStringToBoolArray(Convert.ToString(a, 2));
                            tempBitArray[col] = b;
                            for (int r = bitOffsetInByte; r < 8; r++) {
                                tempBitArray[col][r] = false;
                            }
                        } else {
                            byte a = GetColorData(pixel, col);
                            bool[] b = ConvertStringToBoolArray(Convert.ToString(a, 2));
                            tempBitArray[col] = b;

                            for (int r = bitOffsetInByte; r < 8; r++) {
                                if (bitIndex < messageInBits.Count) {
                                    tempBitArray[col][r] = messageInBits[bitIndex++];
                                }
                            }
                        }
                    }
                    addjustedImage.SetPixel(i, j, CreateColorFromBits(tempBitArray));
                }
            }
        }

        private string DecodeData(int chars) {

            int pixelSizeOffset = BitsToFillFullColor(requestedBitsToEncodeSize);

            int bytesToDecode = 1;
            byte[] characterToDecode = new byte[bytesToDecode];
            int currentByteIndex = 0;
            bool[] currentCharacterBits = new bool[8];
            int currentBitIndex = 0;

            string decodedString = "";

            for (int i = 0; i < originalImage.Width; i++) {
                for (int j = 0; j < originalImage.Height; j++) {
                    if (i * Width + j < pixelSizeOffset) {
                        continue;
                    } // skip size bits

                    Color pixel = originalImage.GetPixel(i, j);
                    for (int col = 0; col < 3; col++) {
                        byte a = GetColorData(pixel, col);
                        bool[] b = ConvertStringToBoolArray(Convert.ToString(a, 2).PadLeft(8, '0'));
                        for (int q = 8 - usedBits; q < 8; q++) {
                            currentCharacterBits[currentBitIndex++] = b[q];
                            if (currentBitIndex == 8) {
                                characterToDecode[currentByteIndex++] = (byte) GetIntFromBitArray(currentCharacterBits);
                                if (currentByteIndex == bytesToDecode) {
                                    string character = Encoding.ASCII.GetString(characterToDecode, 0, bytesToDecode);
                                    decodedString += character;
                                    currentByteIndex = 0;
                                    if (decodedString.Length >= chars)
                                        return Reverse(decodedString);
                                }
                                currentBitIndex = 0;
                            }
                        }
                    }
                }
            }
            return "";
        }

        private int DecodeSize() {
            usedBits = (int) numericUpDown1.Value;

            bool[] collectedSizeBitsFromImage = new bool[BitsToFillFullColor(requestedBitsToEncodeSize)];

            int collectingBitIndex = 0;
            for (int i = 0; i < originalImage.Width; i++) {
                if (collectingBitIndex == collectedSizeBitsFromImage.Length) break;
                for (int j = 0; j < originalImage.Height; j++) {
                    if (collectingBitIndex == collectedSizeBitsFromImage.Length) break;
                    Color pixel = originalImage.GetPixel(i, j);
                    for (int col = 0; col < 3; col++) {

                        if (collectingBitIndex == collectedSizeBitsFromImage.Length) break;
                        byte a = GetColorData(pixel, col);
                        bool[] b = ConvertStringToBoolArray(Convert.ToString(a, 2).PadLeft(8, '0'));
                        for (int q = 8 - usedBits; q < 8; q++) {
                            collectedSizeBitsFromImage[collectingBitIndex++] = b[q];

                            if (collectingBitIndex == collectedSizeBitsFromImage.Length) break;
                        }

                    }
                }
            }
            int messageSize = GetIntFromBitArray(collectedSizeBitsFromImage);

            return messageSize / 8;
        }

        #endregion

        #region Pixel value differencing 

        private Range[] ranges = new Range[6] {
            new Range(0,7,3),
            new Range(8,15,3),
            new Range(16,31,4),
            new Range(32,63,5),
            new Range(64,127,6),
            new Range(128,255,7),
        };

        private void EncodeUsingPvd() {

            string message = richTextBox1.Text;
            var bytesToSave = Encoding.ASCII.GetBytes(message);
            var messageInBits = new BitArray(bytesToSave);

            if (BitConverter.IsLittleEndian) {
                messageInBits = ReverseBitArray(messageInBits);
            }
            int savedBits = 0;

            byte[][,] RGB = new byte[3][,];
            byte[,] R = new byte[originalImage.Width, originalImage.Height];
            byte[,] G = new byte[originalImage.Width, originalImage.Height];
            byte[,] B = new byte[originalImage.Width, originalImage.Height];
            RGB[0] = R; RGB[1] = G; RGB[2] = B;
            int[] maxBits = { 5, 3, 8 };
            Range range;
            bool[] bitsToWrite;

            Color pixel;
            for (int i = 0; i < originalImage.Width; i++) {
                for (int j = 0; j < originalImage.Height; j++) {
                    pixel = originalImage.GetPixel(i, j);
                    R[i, j] = pixel.R;
                    G[i, j] = pixel.G;
                    B[i, j] = pixel.B;
                }
            }

            for (int i = 0; i < originalImage.Width; i++) {
                for (int j = 0; j < originalImage.Height - 1; j += 2) {
                    for (int comp = 0; comp < 3; comp++) {
                        range = GetRangeChecked(RGB[comp][i, j], RGB[comp][i, j + 1], maxBits[comp]);

                        if (range.usable) {
                            if (TryToTrimBorders) {
                                int max = Math.Abs(range.max - range.diff);
                                double maxToAdd = max / 2.0;
                                if (!IsByteUsable(RGB, comp, i, j, range.diff, maxToAdd)) {
                                    continue;
                                }
                            }

                            bitsToWrite = new bool[range.bits];
                            int savedBitsLocal = 0;
                            string debug = "";
                            for (int k = 0; k < range.bits; k++) {
                                bitsToWrite[k] =
                                        savedBits + savedBitsLocal < messageInBits.Length ?
                                        messageInBits[savedBits + savedBitsLocal] :
                                        false;
                                savedBitsLocal++;
                                debug += bitsToWrite[k] ? "1" : "0";
                            }

                            int dec = GetIntFromBitArray(bitsToWrite);
                            dec += range.min;
                            int m = Math.Abs(dec - range.diff);

                            double toAdd = m / 2.0;

                            if (RGB[comp][i, j] >= RGB[comp][i, j + 1] && dec > range.diff) {
                                RGB[comp][i, j] += (byte) Math.Ceiling(toAdd);
                                RGB[comp][i, j + 1] -= (byte) Math.Floor(toAdd);
                            } else if (RGB[comp][i, j] < RGB[comp][i, j + 1] && dec > range.diff) {
                                RGB[comp][i, j] -= (byte) Math.Ceiling(toAdd);
                                RGB[comp][i, j + 1] += (byte) Math.Floor(toAdd);
                            } else if (RGB[comp][i, j] >= RGB[comp][i, j + 1] && dec <= range.diff) {
                                RGB[comp][i, j] -= (byte) Math.Ceiling(toAdd);
                                RGB[comp][i, j + 1] += (byte) Math.Floor(toAdd);
                            } else if (RGB[comp][i, j] < RGB[comp][i, j + 1] && dec <= range.diff) {
                                RGB[comp][i, j] += (byte) Math.Ceiling(toAdd);
                                RGB[comp][i, j + 1] -= (byte) Math.Floor(toAdd);
                            }
                            savedBits += savedBitsLocal;
                        }

                    }
                    // SAVE 2 PIXELS TO IMAGE
                    addjustedImage.SetPixel(i, j, Color.FromArgb(RGB[0][i, j], RGB[1][i, j], RGB[2][i, j]));
                    addjustedImage.SetPixel(i, j + 1, Color.FromArgb(RGB[0][i, j + 1], RGB[1][i, j + 1], RGB[2][i, j + 1]));


                }
            }

            TryToSaveResultImage();
        }

        private bool IsByteUsable(byte[][,] RGB, int comp, int i, int j, int diff, double maxToAdd) {
            if (diff % 2 == 1) {
                if (!CheckForBorders(RGB[comp][i, j] + (byte) Math.Ceiling(maxToAdd)))
                    return false;

                if (!CheckForBorders(RGB[comp][i, j + 1] - (byte) Math.Floor(maxToAdd)))
                    return false;
            } else {
                if (!CheckForBorders(RGB[comp][i, j] - (byte) Math.Floor(maxToAdd)))
                    return false;

                if (!CheckForBorders(RGB[comp][i, j + 1] + (byte) Math.Ceiling(maxToAdd)))
                    return false;
            }

            return true;
        }

        private bool CheckForBorders(int number) {
            if (number < 0 || number > 255)
                return false;
            return true;
        }

        private Range GetRangeChecked(int p1, int p2, int maxBits) {
            int diff = Math.Abs(p1 - p2);
            var range = GetRange(diff);
            range.diff = diff;
            range.usable = range.bits <= maxBits;
            return range;
        }

        private Range GetRange(int b) {
            return ranges.First(range => range.max >= b);
        }

        private void DecodeUsingPvd() {

            string decodedString = "";
            bool[] currentCharacterBits = new bool[8];
            int currentBitIndex = 0;

            int foundEmptySequence = 0;

            int bytesToDecode = 1;
            byte[] characterToDecode = new byte[bytesToDecode];
            int currentByteIndex = 0;

            byte[][,] RGB = new byte[3][,];
            byte[,] R = new byte[originalImage.Width, originalImage.Height];
            byte[,] G = new byte[originalImage.Width, originalImage.Height];
            byte[,] B = new byte[originalImage.Width, originalImage.Height];
            RGB[0] = R; RGB[1] = G; RGB[2] = B;
            int[] maxBits = { 5, 3, 8 };
            Range range;

            Color pixel;
            for (int i = 0; i < originalImage.Width; i++) {
                for (int j = 0; j < originalImage.Height; j++) {
                    pixel = originalImage.GetPixel(i, j);
                    R[i, j] = pixel.R;
                    G[i, j] = pixel.G;
                    B[i, j] = pixel.B;
                }
            }

            for (int i = 0; i < originalImage.Width; i++) {
                for (int j = 0; j < originalImage.Height - 1; j += 2) {
                    for (int comp = 0; comp < 3; comp++) {
                        range = GetRangeChecked(RGB[comp][i, j], RGB[comp][i, j + 1], maxBits[comp]);
                        if (range.usable) {

                            if (TryToTrimBorders) {
                                int max = Math.Abs(range.max - range.diff);
                                double maxToAdd = max / 2.0;
                                if (!IsByteUsable(RGB, comp, i, j, range.diff, maxToAdd)) {
                                    continue;
                                }
                            }
                            //Debug.WriteLine($"READING FROM {GetByComp(comp)} RANGE { range }");

                            int dec = range.diff - range.min;
                            string test = "";

                            string binary = Convert.ToString(dec, 2).PadLeft(8, '0');
                            binary = binary.Substring(8 - range.bits);

                            bool[] aa = ConvertStringBinaryToBoolArray(binary);

                            for (int q = 0; q < aa.Length; q++)
                                test += aa[q] ? "1" : "0";
                            //Debug.WriteLine(aa.Length + " : " + dec + " : " + test);

                            for (int k = 0; k < aa.Length; k++) {
                                currentCharacterBits[currentBitIndex++] = aa[k];
                                if (currentBitIndex == 8) {
                                    characterToDecode[currentByteIndex++] = (byte) GetIntFromBitArray(currentCharacterBits);
                                    if (currentByteIndex == bytesToDecode) {

                                        string character = Encoding.ASCII.GetString(characterToDecode, 0, bytesToDecode);

                                        if (character[0] == '\0')
                                            foundEmptySequence++;
                                        else
                                            foundEmptySequence = 0;
                                        if (foundEmptySequence == 3) {
                                            DisplayDecodedString(decodedString);
                                            return;
                                        }

                                        decodedString += character;
                                        currentByteIndex = 0;

                                    }
                                    currentBitIndex = 0;
                                }
                            }
                        }
                    }
                }
            }
            DisplayDecodedString(decodedString);
        }

        private void DisplayDecodedString(string decodedString) {
            decodedString = decodedString.Replace('\0', '_');
            while (decodedString.Contains("_")) decodedString = decodedString.Replace("_", "");
            //decodedString = string.Join("", decodedString.Reverse());
            decodedString = Reverse(decodedString);
            Debug.WriteLine(decodedString);
            richTextBox1.Text = decodedString;
        }

        #endregion

        #endregion

        #region Form Events 

        private void codeRbtn_CheckedChanged(object sender, EventArgs e) {
            ChangedEncodingCoding();
        }

        private void decodeRbtn_CheckedChanged(object sender, EventArgs e) {
            ChangedEncodingCoding();
        }

        private void loadImageButton_Click(object sender, EventArgs e) {
            openFileDialog1.InitialDirectory = workingDirectory;
            openFileDialog1.Filter = "Image files (*.png;*.jpg,*.jpeg)|*.png;*.jpg;*.jpeg";
            var dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK) {
                originalImagePath = openFileDialog1.FileName;
                LoadImage(originalImagePath);
            }
        }

        private void originalImageButton_Click(object sender, EventArgs e) {
            if (originalImage == null) {
                MessageBox.Show("Není naètený žádný obrázek");
                return;
            }
            loadedImage.BackgroundImage = originalImage;
            button1.Visible = false;
        }

        private void addjustedImageButton_Click(object sender, EventArgs e) {
            if (originalImage == null) {
                MessageBox.Show("Není naètený žádný obrázek");
                return;
            }
            loadedImage.BackgroundImage = addjustedImage;
            button1.Visible = true;
        }

        private void startButton_Click(object sender, EventArgs e) {
            if (!IsExpertiment && (encoding && string.IsNullOrEmpty(richTextBox1.Text))) {
                MessageBox.Show("Zpráva je prázdná");
                return;
            }

            if (encoding && currentMessageSize > availableMessageSize) {
                MessageBox.Show($"Zpráva je moc velká na tento obrázek, nepodraøilo by se uložit {UnitConverter.SizeSuffix(currentMessageSize - availableMessageSize)} zprávy");
                return;
            }

            if (originalImage == null) {
                MessageBox.Show($"Není naèten žádný obrázek!");
                return;
            }
            if (encoding) {
                StartEncoding();
            } else {
                StartDecoding();
            }
        }

        private void readFromFileButton_Click(object sender, EventArgs e) {

            if (!string.IsNullOrEmpty(richTextBox1.Text)) {
                var really = MessageBox.Show("Urèitì chcete naèíst text ze souboru? Pøijdete o text ve vstupním poli.", "Opravdu?", MessageBoxButtons.YesNo);
                if (really == DialogResult.No || really == DialogResult.Cancel)
                    return;
            }

            openFileDialog1.InitialDirectory = "C:\\Users\\Vratislav\\Pictures\\";
            openFileDialog1.Filter = "Text files (*.txt)|*.txt";
            var dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK) {

                try {
                    StreamReader sr = new StreamReader(openFileDialog1.FileName);
                    string message = sr.ReadToEnd();
                    richTextBox1.Text = message;
                } catch (Exception ex) {
                    MessageBox.Show("Chyba! " + ex.Message);
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e) {
            CalculateRequestedSize();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            CalculateAvailableSize();
        }

        private void saveToFileButton_Click(object sender, EventArgs e) {
            saveFileDialog1.InitialDirectory = workingDirectory;
            saveFileDialog1.Filter = "Image PNG files (*.png;*.jpg)|*.png;*.jpg";
            var dr = saveFileDialog1.ShowDialog();
            if (dr == DialogResult.OK) {
                pathTextbox.Text = saveFileDialog1.FileName;
            }
        }

        private void ChangedEncodingCoding() {
            encoding = codeRbtn.Checked;
            startButton.Text = encoding ? "Kódovat" : "Dekódovat";
        }

        private void ChangeAlgorithm() {
            if (lsbRbtn.Checked) {
                label2.Enabled = label2.Visible = true;
                label7.Enabled = label7.Visible = true;
                numericUpDown1.Enabled = numericUpDown1.Visible = true;
                checkBox1.Enabled = checkBox1.Visible = false;
                algorithm = "lsb";
            } else {
                label2.Enabled = label2.Visible = false;
                label7.Enabled = label7.Visible = false;
                numericUpDown1.Enabled = numericUpDown1.Visible = false;
                checkBox1.Enabled = checkBox1.Visible = true;
                algorithm = "pvd";
            }

        }

        private void LoadImage(string originalImagePath) {
            try {
                originalImage = (Bitmap) Image.FromFile(originalImagePath);
                addjustedImage = new Bitmap(originalImage);
                loadedImage.BackgroundImage = originalImage;
                CalculateAvailableSize();
            } catch {
                MessageBox.Show("Nepodaøilo se naèíst obrázek!");
            }
        }

        private void TryToSaveResultImage() {
            if (!string.IsNullOrEmpty(pathTextbox.Text)) {
                try {
                    addjustedImage.Save(pathTextbox.Text);
                    Clipboard.SetText(pathTextbox.Text);
                    MessageBox.Show("Uloženo na: " + pathTextbox.Text + "\nCesta je v clipboardu.");
                } catch (Exception e) {
                    MessageBox.Show("Nepodaøilo se uložit " + e.Message);
                }
            } else {
                MessageBox.Show("Nebyla zadána cesta!");
            }
        }

        private void lsbRbtn_CheckedChanged(object sender, EventArgs e) {
            ChangeAlgorithm();
        }

        private void pvdRbtn_CheckedChanged(object sender, EventArgs e) {
            ChangeAlgorithm();
        }
        private void button1_Click(object sender, EventArgs e) {
            TryToSaveResultImage();
        }
        #endregion
    }

    class Range
    {
        public int min;
        public int max;
        public int bits;

        public int diff;
        public bool usable = false;

        public Range(int min, int max, int bits) {
            this.min = min;
            this.max = max;
            this.bits = bits;
        }

        public override string ToString() {
            return $"{min}-{max} -> diff:{diff} -> {bits} bits";
        }
    }
}
