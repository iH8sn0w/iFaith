using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
static class Definitions
{
	public static string iBSSIV = "";
	public static string iBSSKey = "";
	public static string Ramdisk = ".dmg";
	public static bool iREB_mode = false;
	public static string RamdiskIV = "";
	public static string RamdiskKey = "";
	public static string rootfs_size = "xxx";
		//We may or may not need you...
	public static string rootfs_key = "";
	public static bool DumpMode = false;
	public static string CurrentAddr = "0x0";
	public static int i = 0;

	public static bool iRecoveryConnected = false;
	public static void IPSW_vars()
	{
		//
		if (xml_ipsw_md5 == "38638d6056b53f2d87a0f5fcb5584cdd") {
			//iOS 3.1/7C144 -- iPhone 3G[S]
			iDevice = "iPhone 3GS";
			iBSSIV = "421e5f0f481a3344c9f04c45a13482da";
			iBSSKey = "b831608d0ff9f84d3c35d5c3849cd147808763646ce7695c8bbc39adccb112fb";
			Ramdisk = "018-5352-086.dmg";
			RamdiskIV = "74b6379adc29a078a69c502bfc850b51";
			RamdiskKey = "cbc20b0045f8ee317b08f3481f4f255f77ab48745546c7095e8bbb696746638b";
			rootfs = "018-5344-086.dmg";
			rootfs_key = "b9cd10dd88ab615c1963e8aa04950b12dd64e0e5b11ea63c79a02af6db62334c710d21da";
			rootfs_size = "650";
		} else if (xml_ipsw_md5 == "e0c97bdbb9efbf411b22a81327ad48dc") {
			//iOS 3.1.1/7C145 -- iPod Touch 2G
			iDevice = "iPod Touch 2G";
			iBSSIV = "9036da1779c8d2a2a3b460b0a52146b";
			iBSSKey = "1ea15c674bf6086a4a3840c310521dd24";
			Ramdisk = "018-6041-001.dmg";
			RamdiskIV = "8e84afa0b935d969ced792b5c91c4018";
			RamdiskKey = "b3978d4668c85b11e9c16628f3bfe96c";
			rootfs = "018-6027-001.dmg";
			rootfs_key = "abef664a55de10472c076fa633f47a7c3567239e9af3c73dac4c8683c75f3be27b508eb2";
			rootfs_size = "800";
		} else if (xml_ipsw_md5 == "4ad01a2c6fc82bcac2300253b0368f6e") {
			//iOS 3.1.1/7C145 -- iPod Touch 3G
			iDevice = "iPod Touch 3G";
			iBSSIV = "e1b99c5b491a6c9dc2ec421a10853b1e";
			iBSSKey = "5685c34824a7873ef1dca42dec535efbf202e0e90915d2acc8c36a78283671b8";
			Ramdisk = "018-6045-001.dmg";
			RamdiskIV = "663139ba9ff0d04dbbd68534629994b7";
			RamdiskKey = "7345a9977b97937c4fca2f53a1475aff8d5d39e3b91191110b233be4be7d281c";
			rootfs = "018-6030-001.dmg";
			rootfs_key = "de14c16e21ad5bb12fe572ca9400d29a4443ff208ec49c120ad72d6c3269fd5553047cdd";
			rootfs_size = "700";
		} else if (xml_ipsw_md5 == "089769d37b846917394ffe11da9d2e17") {
			//iOS 3.1.2/7D11 -- iPhone 3GS
			iDevice = "iPhone 3GS";
			iBSSIV = "41639d34547ae3dd7921bf3539dba529";
			iBSSKey = "9121de4a038675d92e1a28683b2138b7a3bdb80994273d090398051c7f5af53c";
			Ramdisk = "018-6051-014.dmg";
			RamdiskIV = "fd19726dc6b555b6bb4dbbcd91d1e7c0";
			RamdiskKey = "fb2792b935fb9cd183341cb24539376556f8b7b8f887eb90fcebaa0daf2d6d9c";
			rootfs = "018-6029-014.dmg";
			rootfs_key = "47d76295817f74953f8e557b4917fe2201e9778a9900e43fbf311a83f176fe521b996a4b";
			rootfs_size = "650";
		} else if (xml_ipsw_md5 == "35c66be376201082a005f0a289f26a65") {
			//iOS 3.1.2/7D11 -- iPod Touch 2G
			iDevice = "iPod Touch 2G";
			iBSSIV = "083528a985c2e3f90f8324e1e9dce4e4";
			iBSSKey = "c7af1cfc980b24e2464b70310e2b1713";
			Ramdisk = "018-6141-014.dmg";
			RamdiskIV = "f37bd3f9597c1acba87bfd4029a08ff9";
			RamdiskKey = "de0e35e16213e69cb9c19dc527d9a96c";
			rootfs = "018-6133-014.dmg";
			rootfs_key = "fc68c25f1dcc929f37c2be82b94e4c92b48eac3ddd67adefd462404663265e3dca43a930";
			rootfs_size = "800";
		} else if (xml_ipsw_md5 == "13938eaca91e12e7cefb47717e7cadc8") {
			//iOS 3.1.2/7D11 -- iPod Touch 3G
			iDevice = "iPod Touch 3G";
			iBSSIV = "8caeaf0aac65bbf6f3f345f7a0c95f76";
			iBSSKey = "891747c374707b5cc6f5d4ffe9f0f24cc166858affc15f34d4bd0f41cb3077a0";
			Ramdisk = "018-6155-014.dmg";
			RamdiskIV = "1a388bbc8ceed19a46474af07d58480b";
			RamdiskKey = "e6361c6c999807f6069871b43b2421841057dc200f5543277bf8a7e07c68f400";
			rootfs = "018-6152-014.dmg";
			rootfs_key = "1e05ef21821280869d4029a2328836b9f60bc63907c6e951c0f1c80c2d8c66aef5c39a44";
			rootfs_size = "700";
		} else if (xml_ipsw_md5 == "4117e4b22565e69205a84e9eeef0583e") {
			//iOS 3.1.3/7E18 -- iPhone 3GS
			iDevice = "iPhone 3GS";
			iBSSIV = "bb70b0109c0f6a323dba00df5806b111";
			iBSSKey = "a95c62b3665493c92eebf3d471ea5949827fd9aab4248cd99d66bc2edf7ac4fe";
			Ramdisk = "018-6495-022.dmg";
			RamdiskIV = "50a5d7418e3091a2c1d878495a6dbc6a";
			RamdiskKey = "217c7c38387264f2a2fef7a661d1bbeb705e7c90581c5b73055fe44f5bbc0498";
			rootfs = "018-6483-022.dmg";
			rootfs_key = "9b3fd35bad7d5307d85ce4b38b8e56bd680ef5a72a8f3b615f8d4f16c14bdcf3c3b24c6c";
			rootfs_size = "650";
		} else if (xml_ipsw_md5 == "33df8d6ae5d8a695bba267ae89fe37f1") {
			//iOS 3.1.3/7E18 -- iPod Touch 2G
			iDevice = "iPod Touch 2G";
			iBSSIV = "656dd7144de645da835aaedf9354e153";
			iBSSKey = "6021f2e35db67c4aaec9c1f27a4226bf";
			Ramdisk = "018-6508-014.dmg";
			RamdiskIV = "2e7aa9f6dc823657f1c930a00db8efe1";
			RamdiskKey = "9781d55350f58c3ff72c7a3e50b70288";
			rootfs = "018-6481-015.dmg";
			rootfs_key = "2360d83b606481a5ca080fe7a6fc64f8d5a5556413dfcf3e1277fe564734ee0b188798b8";
			rootfs_size = "800";
		} else if (xml_ipsw_md5 == "a73de2cfafef3463e9afa491f20c5213") {
			//iOS 3.1.3/7E18 -- iPod Touch 3G
			iDevice = "iPod Touch 3G";
			iBSSIV = "cc884d83083849382cb33c59d61ca427";
			iBSSKey = "1750aa8b05b8bf78c76d35784f690b3684bf5107485db09a17e284974fbd695a";
			Ramdisk = "018-6496-015.dmg";
			RamdiskIV = "ed40b5cf7bc9f3eb2db62b8f11d86a7d";
			RamdiskKey = "4ef57007bebb68849b715bfdf37d53d73192b1f5c2228947268c1deb4cad689d";
			rootfs = "018-6522-015.dmg";
			rootfs_key = "1402974cba4702e831fb821ef9090229f7bad6fd3084fa99bfc8a76de4d839f9bf4533eb";
			rootfs_size = "670";
		} else if (xml_ipsw_md5 == "2912cefa0304e5430594c576ad88d398") {
			//iOS 3.2/7B367 -- iPad 1G
			iDevice = "iPad 1G";
			iBSSIV = "b83ba3ecc1919d4dc80a560230b80910";
			iBSSKey = "eb3c9eabd45eb5701fe1998d570fa38a31ba2807918345a5c7efe0fff7ce1bea";
			Ramdisk = "018-7226-009.dmg";
			RamdiskIV = "9c051576ddd94f48c324cf7ac3197fe1";
			RamdiskKey = "31e7ecd9c364414205a8fa0092cc80c0d67eae40e75ffa27b37048c42335a106";
			rootfs = "018-7223-007.dmg";
			rootfs_key = "2be8f3a0a02f2d259c9b297cb2d156a85adf79fed4ffe88c546a42c2a47aa55f70cadebd";
			rootfs_size = "1024";
		} else if (xml_ipsw_md5 == "5ccf846d96a677f42ac183f5a137dc92") {
			//iOS 3.2.1/7B405 -- iPad 1G
			iDevice = "iPad 1G";
			iBSSIV = "3a0ad7cf0f172bc3736af2099a4a89b4";
			iBSSKey = "0dcdc5bdc0d991020222c0e7b7d11305466f0ef831964c5fc9325f05e32b1a09";
			Ramdisk = "018-7823-011.dmg";
			RamdiskIV = "3ed149b2f67690e5376880fc0323fbe2";
			RamdiskKey = "aa616f53beff0bd86dfb7aa53954614395fc13e31be97fb16e0f886f0d6cdcfc";
			rootfs = "018-7825-011.dmg";
			rootfs_key = "c3d15c6dc3b289db4d90b59199c485486043bb534c14d21993e35f68f2c6c1804a9125a8";
			rootfs_size = "1024";
		} else if (xml_ipsw_md5 == "cf6d93fffdc60dcca487a80004d250fa") {
			//iOS 3.2.2/7B500 -- iPad 1G
			iDevice = "iPad 1G";
			iBSSIV = "3a0ad7cf0f172bc3736af2099a4a89b4";
			iBSSKey = "0dcdc5bdc0d991020222c0e7b7d11305466f0ef831964c5fc9325f05e32b1a09";
			Ramdisk = "018-8375-001.dmg";
			RamdiskIV = "309d4fc146b47f43e8bc27133d7ce6bc";
			RamdiskKey = "f45eaec8b6c17af7c6071635a313572b1b0208bc445cfb6c1d60b6bf377e6653";
			rootfs = "018-8370-001.dmg";
			rootfs_key = "18ae1e76e7bcf6478321f42888606ca2d998cffab1ee8c7ca6b15d57b1a7254f8a608823";
			rootfs_size = "1024";
		} else if (xml_ipsw_md5 == "f9819ad9a52324ac6f10e4a0ea581cbd") {
			//iOS 4.0/8A293 -- iPhone 3GS
			iDevice = "iPhone 3GS";
			iBSSIV = "d044caa6b4fe10d75e081f1950b986e7";
			iBSSKey = "16785e6ec40dcd29ce5cfb46e18d6031ac2834f4efccdce6deea967c006c8018";
			Ramdisk = "018-6461-399.dmg";
			RamdiskIV = "d506ed68e4cd9955fe09be284ff5c60c";
			RamdiskKey = "adb1b9da63b54cb07e8c4a22481bf2c7dee17519506085b07b1a8057a8980003";
			rootfs = "018-5534-582.dmg";
			rootfs_key = "5d79765bc3233cbee58727c17a9487e5dc1e38400c2a98c30997bb02d00e97ae3ce5fab8";
			rootfs_size = "800";
		} else if (xml_ipsw_md5 == "8717be79fb38cd83aa5e5956eb0608b7") {
			//iOS 4.0/8A293 -- iPhone 4
			iDevice = "iPhone 4";
			iBSSIV = "91f94e5d726a2d2f2c7ffad58d4f3b77";
			iBSSKey = "d05c3c40db40e738926f811b8b1314038d26096c4102461698a49098c47a3fe6";
			Ramdisk = "018-6306-403.dmg";
			RamdiskIV = "0ab135879934fdd0d689b3d0f8cf8374";
			RamdiskKey = "62aabe3e763eb3669b4922468be2acb787199c6b0ef8ae873c312e458d9b9be3";
			rootfs = "018-6303-385.dmg";
			rootfs_key = "8b2915719d9f90ba5521faad1eadbb3d942991bd55e5a0709f26e9db3931517e054afa50";
			rootfs_size = "1024";
		} else if (xml_ipsw_md5 == "41dd8ab40159a13d7be42cd7e5f3a479") {
			//iOS 4.0/8A293 -- iPod Touch 2G
			iDevice = "iPod Touch 2G";
			iBSSIV = "93463b2dc308eecb29a1732312a9f0c9";
			iBSSKey = "47a818705e66dc67c42002553a10adb2";
			Ramdisk = "018-6462-368.dmg";
			RamdiskIV = "b2b8949bd33b65f22bd0c7e55ccc836b";
			RamdiskKey = "1e3e144a55fa8d1782db5153796238f0";
			rootfs = "018-5533-585.dmg";
			rootfs_key = "fcada08311f553b2d7194c97922e01c821b632bf62e64500056ea37e56343e6131a9839b";
			rootfs_size = "800";
		} else if (xml_ipsw_md5 == "6b9d65c9f63792968bad57e44a73434f") {
			//iOS 4.0/8A293 -- iPod Touch 3G
			iDevice = "iPod Touch 3G";
			iBSSIV = "27c5becf5fea4936b44ab27f22ede19b";
			iBSSKey = "2e9374bcf55a4d14495141eddfdb9cca039a7dc0913676586cb3e19bcde135db";
			Ramdisk = "018-6307-378.dmg";
			RamdiskIV = "0b02c0daefed9ec93e52a20b9575b754";
			RamdiskKey = "8d9f400ee20e54947b143a64d0356c215800b83191fed8e73f3c0e84d1e32d37";
			rootfs = "018-5530-583.dmg";
			rootfs_key = "ec6eb0268c4e9f8ab9d003f601e8f4b36f4fc4311c61e5ebed07ce718424ffee7e7d924d";
			rootfs_size = "800";
		} else if (xml_ipsw_md5 == "a3104ca3b72a91bc7eff037ee320ecc5") {
			//iOS 4.0.1/8A306 -- iPhone 3GS
			iDevice = "iPhone 3GS";
			iBSSIV = "d044caa6b4fe10d75e081f1950b986e7";
			iBSSKey = "16785e6ec40dcd29ce5cfb46e18d6031ac2834f4efccdce6deea967c006c8018";
			Ramdisk = "018-8234-001.dmg";
			RamdiskIV = "5e8b26560b3f18f4f1aa76bc6d526d86";
			RamdiskKey = "53896fea899c222b84791128d383268b579b5acd5230d2b35ed066294b2273e8";
			rootfs = "018-8228-001.dmg";
			rootfs_key = "5d9385452d9ce0fe0185dfc59a7cbb1015d086ce53ff769e78dd45bc6e4eeb48c60e2952";
			rootfs_size = "800";
		} else if (xml_ipsw_md5 == "40ebacb47fb32d7f33ba0fd596e150e9") {
			//iOS 4.0.1/8A306 -- iPhone 4
			iDevice = "iPhone 4";
			iBSSIV = "91f94e5d726a2d2f2c7ffad58d4f3b77";
			iBSSKey = "d05c3c40db40e738926f811b8b1314038d26096c4102461698a49098c47a3fe6";
			Ramdisk = "018-8235-001.dmg";
			RamdiskIV = "5dfa31e28674d44e19ac05148ae7f668";
			RamdiskKey = "79cb7f8c64b6302a58a63a693ce0582df269fad68f6eb9d85340c1f75dbe89d6";
			rootfs = "018-8229-001.dmg";
			rootfs_key = "ebd8aea30e78053112c4062690723fc5ee8e53865d4d6591b64a08216337c5a7aefbc806";
			rootfs_size = "1024";
		} else if (xml_ipsw_md5 == "9cb5684457fb41886827d727d91313c3") {
			//iOS 4.0.2/8A400 -- iPhone 3GS
			iDevice = "iPhone 3GS";
			iBSSIV = "d044caa6b4fe10d75e081f1950b986e7";
			iBSSKey = "16785e6ec40dcd29ce5cfb46e18d6031ac2834f4efccdce6deea967c006c8018";
			Ramdisk = "018-8391-002.dmg";
			RamdiskIV = "987c49205269a380d2b26be9c601a610";
			RamdiskKey = "87c9676ed157488e49fd638b179e8f6106396d8ab85e730031a0987918e591d5";
			rootfs = "018-8378-002.dmg";
			rootfs_key = "812288d52a0845a41c3cd61e6b5a0f85731ce3fc04aa631895d40ca77d8f325ff02c70e9";
			rootfs_size = "800";
		} else if (xml_ipsw_md5 == "790b24fe7515084f457ce413618b2709") {
			//iOS 4.0.2/8A400 -- iPhone 4
			iDevice = "iPhone 4";
			iBSSIV = "91f94e5d726a2d2f2c7ffad58d4f3b77";
			iBSSKey = "d05c3c40db40e738926f811b8b1314038d26096c4102461698a49098c47a3fe6";
			Ramdisk = "018-8393-002.dmg";
			RamdiskIV = "b662a1cd9b51d43029624adf2c65e56a";
			RamdiskKey = "7f3ed4bd7773bd07f9f4a697b56bc85dc0040907cf1282077d477e9b5c92c53f";
			rootfs = "018-8380-002.dmg";
			rootfs_key = "28bded3ee52eda2f36a241009a493db357b8f19543c07bd3820a35498a1788ce4aa0c54c";
			rootfs_size = "1024";
		} else if (xml_ipsw_md5 == "e706efcf835de9fcf6f96c7a420a7a22") {
			//iOS 4.0.2/8A400 -- iPod Touch 2G
			iDevice = "iPod Touch 2G";
			iBSSIV = "93463b2dc308eecb29a1732312a9f0c9";
			iBSSKey = "47a818705e66dc67c42002553a10adb2";
			Ramdisk = "018-8101-012.dmg";
			RamdiskIV = "5b65f57eddaffa682585258f5fab1499";
			RamdiskKey = "637842bc3a51d8cd5984d7d685bd6dd7";
			rootfs = "018-8080-012.dmg";
			rootfs_key = "5d1655d3cd7c6ffb4a5e48a52ea8a265579c655aa39ed8613239e57f20f132e4e3b5ffa1";
			rootfs_size = "800";
		} else if (xml_ipsw_md5 == "dc7741b9e4353895c3910237a5b10a4d") {
			//iOS 4.0.2/8A400 -- iPod Touch 3G
			iDevice = "iPod Touch 3G";
			iBSSIV = "27c5becf5fea4936b44ab27f22ede19b";
			iBSSKey = "2e9374bcf55a4d14495141eddfdb9cca039a7dc0913676586cb3e19bcde135db";
			Ramdisk = "018-8095-012.dmg";
			RamdiskIV = "66fbf227d7a74cf39cbc149f0ebb3926";
			RamdiskKey = "0108ebb075ca534df759e849a47d79ad757f39a51a3352a9d3d760ce1c6609af";
			rootfs = "018-8106-012.dmg";
			rootfs_key = "4e164b7c39c8e0234787f7b9ae204adf1e3a66d472f1dce1db41e42ba87d1ff5722a7689";
			rootfs_size = "800";
		} else if (xml_ipsw_md5 == "e07bee3c03e7a18e5d75fcaa23db17b5") {
			//iOS 4.1/8B117 -- iPhone 3GS
			iDevice = "iPhone 3GS";
			iBSSIV = "966fdb6312a3cd35703e7a1e8bb4cce6";
			iBSSKey = "81d26076947f2a50c0d31766f7e5f3b73ec198a1e0c50064a03b9e74bd0cbf91";
			Ramdisk = "018-7080-079.dmg";
			RamdiskIV = "214388b7e0589464bf59966524ae2ea4";
			RamdiskKey = "581f739963fc3fdbf70dfc695b35d43662a0069b501cb715264c32428e759cba";
			rootfs = "018-7061-122.dmg";
			rootfs_key = "01155a88dc41d6bdb6ba368719853e7e68fb0076dbfaafe8e0801256c724b103f2e271ca";
			rootfs_size = "800";
		} else if (xml_ipsw_md5 == "ac3031a7b5c013d6a09952b691985878") {
			//iOS 4.1/8B117 -- iPhone 4
			iDevice = "iPhone 4";
			iBSSIV = "c2c5416472e5a0d6f0a25a123d5a2b1c";
			iBSSKey = "1fbc7dcafaec21a150a51eb0eb99367550e24a077b128831b28c065e61f894a0";
			Ramdisk = "018-7082-092.dmg";
			RamdiskIV = "103ae8786d55bebdea996a56706641c9";
			RamdiskKey = "a80b3c27041f09d4554bbf4af59dd5bcea38bd4fe2faf82d8d6f62853ec6b337";
			rootfs = "018-7063-114.dmg";
			rootfs_key = "2ab6aea67470994ec3453791ac75f6497c081edd1991e560a61dd666ac4b73f43c781739";
			rootfs_size = "830";
		} else if (xml_ipsw_md5 == "9f8a1978f053ec96926e535bb57ac171") {
			//iOS 4.1/8B117 -- iPod Touch 2G
			iDevice = "iPod Touch 2G";
			iBSSIV = "f7ed98e14e9f7f01397639a4424ef175";
			iBSSKey = "abcc0848b65d7e2e675f8030ea37f325";
			Ramdisk = "018-7103-078.dmg";
			RamdiskIV = "58df0d0655bbdda2a0f1c09333940701";
			RamdiskKey = "fbf443110eb11d8d1aacdbe39167de09";
			rootfs = "018-7058-113.dmg";
			rootfs_key = "aec4d2f3c6a4e70ea6cea074f65812d2a34b180cc92b817edcd867167e7a91c5beb942f0";
			rootfs_size = "800";
		} else if (xml_ipsw_md5 == "f3877c6f309730ee31297a06c7a9e82c") {
			//iOS 4.1/8B117 -- iPod Touch 3G
			iDevice = "iPod Touch 3G";
			iBSSIV = "45e620eb1cabe17cb5e25018e628160e";
			iBSSKey = "1b45e1726a88d3e89a203a59544b853c531758dd3c6f15fd9fa24cfa4ebc949d";
			Ramdisk = "018-7081-078.dmg";
			RamdiskIV = "1edc4378f31ce728a2412ff93c78b8dd";
			RamdiskKey = "425f8a7bdac80a9678c78317a0ddbb91abb52a2fd1ff45f46c3e6db392155db9";
			rootfs = "018-7116-114.dmg";
			rootfs_key = "69e2ca7a250765c95a703081d1195e681fbe82f31162b79fd2b70754629b0352694b9eda";
			rootfs_size = "800";
		} else if (xml_ipsw_md5 == "2e634d16d0e01ef70070c9a289e488ca") {
			//iOS 4.1/8B117 -- iPod Touch 4
			iDevice = "iPod Touch 4";
			iBSSIV = "c58929f652c1c086f70f941f3bb31058";
			iBSSKey = "358e67475d675410517ccbfcbbc38fa4c56d0e892b627460851a1fa5e9b430ab";
			Ramdisk = "018-7082-092.dmg";
			RamdiskIV = "103ae8786d55bebdea996a56706641c9";
			RamdiskKey = "a80b3c27041f09d4554bbf4af59dd5bcea38bd4fe2faf82d8d6f62853ec6b337";
			rootfs = "018-7062-093.dmg";
			rootfs_key = "e7de54b25167afc66e381ade1d5e25c6392757497cfd92826a3111772731ba0b70742b90";
			rootfs_size = "850";
		} else if (xml_ipsw_md5 == "0564fcd3f53dd6262b9eb636b7fbe540") {
			//iOS 4.1/8B118 -- iPod Touch 4
			iDevice = "iPod Touch 4";
			iBSSIV = "c58929f652c1c086f70f941f3bb31058";
			iBSSKey = "358e67475d675410517ccbfcbbc38fa4c56d0e892b627460851a1fa5e9b430ab";
			Ramdisk = "018-9394-001.dmg";
			RamdiskIV = "e869e35fae9877bcba930e66e1536fc6";
			RamdiskKey = "205d0b4636400b08cd0b86a4afdec9d7fc79267f3146a00ce9bb4f3d987a1547";
			rootfs = "018-9391-001.dmg";
			rootfs_key = "770b58765a3345004528fd9a2cbb7c3105137d0bd3a134a24679e6e173f32636d0485d06";
			rootfs_size = "850";
		} else if (xml_ipsw_md5 == "35c8ab4b7e70ab0e47e2f5981e52ba55") {
			//iOS 4.1 (4.0)/8M89 -- Apple TV 2
			iDevice = "Apple TV 2";
			iBSSIV = "921a1405543996714f80d2fb1f8d0242";
			iBSSKey = "e31d511699eb9564c7174932e595e75f08aaa11d392d9026f28fe60ede336fc2";
			Ramdisk = "018-8613-062.dmg";
			RamdiskIV = "193f5c4a3a8fb0643e9cac4020b67e1c";
			RamdiskKey = "1cdaede20dfc5f2299f23c237688b341390088c71b8aa63793770e5c1f3e6dc9";
			rootfs = "018-8609-066.dmg";
			rootfs_key = "31c700a852f1877c88efc05bc5c63e8c7f081c4cb28d024ed7f9b0dbc98c7e1406e499c6";
			rootfs_size = "800";
		} else if (xml_ipsw_md5 == "d688d2d48c8b054367adef8e7ab4f5ea") {
			//iOS 4.2.1/8C148a -- iPhone 3GS
			iDevice = "iPhone 3GS";
			iBSSIV = "6c69b68fe261460cdc047145f22e6647";
			iBSSKey = "68509ca37881fe7ae9508e559d57c6c54e5307dc883584a264470ed5685ce34f";
			Ramdisk = "038-0082-001.dmg";
			RamdiskIV = "50932f5bd4dbd51fc0073851fe8b073c";
			RamdiskKey = "15a37ae180c0f51d119c9709f244211fc27334b8c7367dd80147f5e5952d0327";
			rootfs = "038-0080-001.dmg";
			rootfs_key = "ec413e58ef2149a2c5a2669d93a4e1a9fe4d7d2f580af2b2ee55c399efc3c22250b8d27a";
			rootfs_size = "850";
		} else if (xml_ipsw_md5 == "93957e7bd21f0549b60a60485c13206a") {
			//iOS 4.2.1/8C148 -- iPhone 4
			iDevice = "iPhone 4";
			iBSSIV = "45bbf0fa98573425fa21dc6e529eba6b";
			iBSSKey = "32398d3d1328ed3f0e1949446a1357585ae1973b3c8434b83df49ac55cf45d06";
			Ramdisk = "038-0032-002.dmg";
			RamdiskIV = "9b20ae16bebf4cf1b9101374c3ab0095";
			RamdiskKey = "06849aead2e9a6ca8a82c3929bad5c2368942e3681a3d5751720d2aacf0694c0";
			rootfs = "038-0019-002.dmg";
			rootfs_key = "b2ee5018ef7d02e45ef67449d9e2ed5f876efae949de64a9a93dbcf7ff9ed84e041e9167";
			rootfs_size = "900";
		} else if (xml_ipsw_md5 == "0045e3543647e23470b84c2c1de96ab1") {
			//iOS 4.2.1/8C148 -- iPod Touch 2G
			iDevice = "iPod Touch 2G";
			iBSSIV = "856c2d9884ab1485cfd75b533246282b";
			iBSSKey = "c3f0f7a3093a74b4ead7fa0b4f80dd38";
			Ramdisk = "038-0049-002.dmg";
			RamdiskIV = "654eb6a2290da5b9b77e6570f2d8ba2b";
			RamdiskKey = "8d53585fa73916d27016abc81e41da82";
			rootfs = "038-0014-002.dmg";
			rootfs_key = "519ec112b4af0a65eab6ea65b222c5b7f605ce52ad9195640e3309de58dd54ab0a0c9607";
			rootfs_size = "800";
		} else if (xml_ipsw_md5 == "25dbf5b3e5ca39edd0aab8fcab888503") {
			//iOS 4.2.1/8C148 -- iPod Touch 3G
			iDevice = "iPod Touch 3G";
			iBSSIV = "962cc5a04ef71269630fd05b7d43f0d6";
			iBSSKey = "f51e414ba2cb4bf68fcd1ea70d420207d4e269d048265b6480764e65af511904";
			Ramdisk = "038-0031-002.dmg";
			RamdiskIV = "9064ae28aef1db52cde6f7568c188766";
			RamdiskKey = "1229ee227f260b8746021d4a46366ec42f987c36d4910a4925e6ca0ce369f69f";
			rootfs = "038-0061-002.dmg";
			rootfs_key = "abd68f16920490865a09e559123db1f471ff19743ad15ea8b970a73e28f5efc6c6e76925";
			rootfs_size = "800";
		} else if (xml_ipsw_md5 == "14d1508954532e91172f8704fd941a93") {
			//iOS 4.2.1/8C148 -- iPod Touch 4
			iDevice = "iPod Touch 4";
			iBSSIV = "5a01a1e31d2ae895690cd279dbd5e3c0";
			iBSSKey = "6d26c20f472d9ed5ab6219e632e35b4c582c1c402104aa39d75471171c88d473";
			Ramdisk = "038-0032-002.dmg";
			RamdiskIV = "9b20ae16bebf4cf1b9101374c3ab0095";
			RamdiskKey = "06849aead2e9a6ca8a82c3929bad5c2368942e3681a3d5751720d2aacf0694c0";
			rootfs = "038-0017-002.dmg";
			rootfs_key = "982437b30d334c744c94b9a73ab70e0fc6ed94c181b2a8b0fde6ee03f2546cc9b2c5b01c";
			rootfs_size = "850";
		} else if (xml_ipsw_md5 == "9402d5f05348fd68c87f885ff4cb4717") {
			//iOS 4.2.1/8C148 -- iPad 1G
			iDevice = "iPad 1G";
			iBSSIV = "873ca0583dbfd0b40d42bd86bca09a07";
			iBSSKey = "619a29ff91bf924bd1edf8efe48d280f5ec85865dd55dfe5f8989a1077f71a0e";
			Ramdisk = "038-0032-002.dmg";
			RamdiskIV = "9b20ae16bebf4cf1b9101374c3ab0095";
			RamdiskKey = "06849aead2e9a6ca8a82c3929bad5c2368942e3681a3d5751720d2aacf0694c0";
			rootfs = "038-0018-002.dmg";
			rootfs_key = "6380bc27ef713750c21759ce770cb6540a8e31fca4c78820fd7be3a02030365a59257582";
			rootfs_size = "850";
		} else if (xml_ipsw_md5 == "3fe1a01b8f5c8425a074ffd6deea7c86") {
			//iOS 4.2.1/8C154 -- Apple TV 2
			iDevice = "Apple TV 2";
			iBSSIV = "03baadf8801e8b7cdcee5a9f53609d0c";
			iBSSKey = "c9f8bd4e52530ec8ef3e2b5926777f624061a38d09f07785287de6e88353f752";
			Ramdisk = "038-0318-001.dmg";
			RamdiskIV = "7c256102d0580b960213540965618b5b";
			RamdiskKey = "5d4e967158ab75ba27ec281bff4e714dacc88123ea4913ae2bee6a719c15496c";
			rootfs = "038-0316-001.dmg";
			rootfs_key = "5407d28e075f5a2e06fddb7ad00123aa5a528bd6c2850d5fa0908a4dcae7dd3e00a9cdb2";
			rootfs_size = "750";
		} else if (xml_ipsw_md5 == "eb3c205debb52c237c37f92335e6369c") {
			//iOS 4.2.6/8E200 -- iPhone 4 (CDMA)
			iDevice = "iPhone 4";
			iBSSIV = "6863087c07128d170db61316205c5a45";
			iBSSKey = "cabd9afa6d7678f3f95d0ccf43d18f54e4ee2e6ac8025b2a528f3b07579ec305";
			Ramdisk = "038-0524-001.dmg";
			RamdiskIV = "c28f3cc7af09a94258a82ea9d1817088";
			RamdiskKey = "cf105a226edc8ff168300eb176d36c5f2de1f712985b7eff12d55868a61288c4";
			rootfs = "038-0520-001.dmg";
			rootfs_size = "850";
			rootfs_key = "723ded674deb1cba56a142542a0b06d2a483297f8056c0cfa70346c0724e1b0e03feded6";
		} else if (xml_ipsw_md5 == "30fc03783453d23aaa0d13f89fd36c28") {
			//iOS 4.2.7/8E303 -- iPhone 4 (CDMA)
			iDevice = "iPhone 4";
			iBSSIV = "6863087c07128d170db61316205c5a45";
			iBSSKey = "cabd9afa6d7678f3f95d0ccf43d18f54e4ee2e6ac8025b2a528f3b07579ec305";
			Ramdisk = "038-0974-004.dmg";
			RamdiskIV = "8d612fbab555c8e8f548898b0e6d3cb0";
			RamdiskKey = "9ca594fef56655a7ad4dc1312dc4a499851d832c2eeb86f5a9ebfabe08ccedb6";
			rootfs = "038-0970-004.dmg";
			rootfs_key = "612f78042ddc5337ab1abecfb59a07e88ed3e80665a035ef02c3c48045057fc29ab0a4b5";
			rootfs_size = "850";
		} else if (xml_ipsw_md5 == "0e0e4bf8f0d7c37b9a252fcbed60ac0c") {
			//iOS 4.2.8/8E401 -- iPhone 4 (CDMA)
			iDevice = "iPhone 4";
			iBSSIV = "6863087c07128d170db61316205c5a45";
			iBSSKey = "cabd9afa6d7678f3f95d0ccf43d18f54e4ee2e6ac8025b2a528f3b07579ec305";
			Ramdisk = "038-1479-003.dmg";
			RamdiskIV = "e9ab0492bb9f14bf17f0810c64e5f874";
			RamdiskKey = "9ea3e9eb1de46882cb2ecdd0b27013c4f91f225e559de446c9a346b52f9cfe4d";
			rootfs = "038-1497-003.dmg";
			rootfs_key = "d8e162215f27c016ed8d1849c6059f99984c766c72cec4a1df63724491c8e5b19c0e6fb2";
			rootfs_size = "850";
		} else if (xml_ipsw_md5 == "87ebb9b2c025fb5f87a4cab0631b1547") {
			//iOS 4.3/8F190-- iPhone 3GS
			iDevice = "iPhone 3GS";
			iBSSIV = "25042db8abe54ba2a740dffd909bd1ed";
			iBSSKey = "377c13630bc9370242e68b28bab1cb1cc21aa8220d3a1c79bc11471e763392d8";
			Ramdisk = "038-0713-006.dmg";
			RamdiskIV = "9f4ca36fb6b30edaebdfbec3c67ce128";
			RamdiskKey = "8bb1fd99c264f46e9b5219cf4817d6e8c0d5915a893a037f31f82bd43f97ce60";
			rootfs = "038-0685-006.dmg";
			rootfs_key = "95028f5804a6d675190adedc0aa91385e17589f720c517615367d69c63e0c969121aaec6";
			rootfs_size = "820";
		} else if (xml_ipsw_md5 == "e0a463bded8f5b1e076b466535b18c75") {
			//iOS 4.3/8F190 -- iPhone 4
			iDevice = "iPhone 4";
			iBSSIV = "37f4d36494ac9d83ab8a9e4936c885f8";
			iBSSKey = "f5e50c94dfee05ed52b4003750007f4c2d1801f7e90e768774ac656dc62c69db";
			Ramdisk = "038-0715-006.dmg";
			RamdiskIV = "d11772b6a3bdd4f0b4cd8795b9f10ad9";
			RamdiskKey = "9873392c91743857cf5b35c9017c6683d5659c9358f35c742be27bfb03dee77c";
			rootfs = "038-0688-006.dmg";
			rootfs_key = "34904e749a8c5cfabecc6c3340816d85e7fc4de61c968ca93be621a9b9520d6466a1456a";
			rootfs_size = "900";
		} else if (xml_ipsw_md5 == "43383f2d5cd181f2af1e01ec62a3f1d6") {
			//iOS 4.3/8F190 -- iPod Touch 3G
			iDevice = "iPod Touch 3G";
			iBSSIV = "54774ea902fe56e59223b2b78d46b975";
			iBSSKey = "7b2c0dcc0a9d4b0bcfe7946d5e4563f5516c69495daa65a13ad630471f453007";
			Ramdisk = "018-7940-131.dmg";
			RamdiskIV = "c4ddd3ed329d243488a99aa50f693d2e";
			RamdiskKey = "26082729ed2afb965b396cccc8f16dc44e0372a9b02095ebf523956edca40a4f";
			rootfs = "018-8011-130.dmg";
			rootfs_key = "cca43b420c4ffefb23a9b5e1605db40df1d89cb13d5951e22b7dda5a35a5cb2dcde85e4a";
			rootfs_size = "800";
		} else if (xml_ipsw_md5 == "0c8cdbbb729508811fa5bd29d8e1143b") {
			//iOS 4.3/8F190 -- iPod Touch 4
			iDevice = "iPod Touch 4";
			iBSSIV = "0fc5175caf46f3328ef43346a25bd1d1";
			iBSSKey = "b482abee8c3e9d5727f96ed39a15d43c7a8c737bc87269a5b95356ec9338f8b8";
			Ramdisk = "038-0715-006.dmg";
			RamdiskIV = "d11772b6a3bdd4f0b4cd8795b9f10ad9";
			RamdiskKey = "9873392c91743857cf5b35c9017c6683d5659c9358f35c742be27bfb03dee77c";
			rootfs = "018-7892-129.dmg";
			rootfs_key = "b5eefbaf0046a79c689ff07e66ee8045f859dab1ee16d44d15606c1918e5afd323f2db07";
			rootfs_size = "900";
		} else if (xml_ipsw_md5 == "9a889ba48bc2715292f199f50c70ed60") {
			//iOS 4.3/8F190 -- iPad 1G
			iDevice = "iPad 1G";
			iBSSIV = "d9df21fc5a610752d6b2d1fc9acee1de";
			iBSSKey = "9f628d5b2a317473405154ad312b2ca2bd56cc1fc0d3eb8c90a1bc96d077e267";
			Ramdisk = "038-0715-006.dmg";
			RamdiskIV = "d11772b6a3bdd4f0b4cd8795b9f10ad9";
			RamdiskKey = "9873392c91743857cf5b35c9017c6683d5659c9358f35c742be27bfb03dee77c";
			rootfs = "038-0687-006.dmg";
			rootfs_key = "890650c3aa3be7c4d6f3473776580acf6781688e6342ed15441a299142fe4c5e933fc89d";
			rootfs_size = "800";
		} else if (xml_ipsw_md5 == "893cdf844a49ae2f7368e781b1ccf6d1") {
			//iOS 4.3/8F202 -- Apple TV 2
			iDevice = "Apple TV 2";
			iBSSIV = "17742baec33113889e5cbfcaa12fb4f0";
			iBSSKey = "998bd521b5b54641fbeb3f73d9959bae126db0bc7e90b7ede7440d3951016010";
			Ramdisk = "038-0946-003.dmg";
			RamdiskIV = "87af8e20133c17d45dab42702eeb136d";
			RamdiskKey = "d9a0258b4f25101b15e260663937c3f6cda748d051bebe09212f51f0be72a89f";
			rootfs = "038-0943-003.dmg";
			rootfs_key = "7fb6a5a1a5d74ceb61180c8740065b79ac87a5c15e554ad4b147ae9e1446254acc9d5e4a";
			rootfs_size = "800";
		} else if (xml_ipsw_md5 == "4726cfb30f322f8cdbb5f20df7ca836f") {
			//iOS 4.3/8F305 -- Apple TV 2
			iDevice = "Apple TV 2";
			iBSSIV = "17742baec33113889e5cbfcaa12fb4f0";
			iBSSKey = "998bd521b5b54641fbeb3f73d9959bae126db0bc7e90b7ede7440d3951016010";
			Ramdisk = "038-0963-007.dmg";
			RamdiskIV = "d3a3b18903be2446fb2902783c258420";
			RamdiskKey = "8225502b126c0e0c3b65454bd0a9ebaef801b49ca3c0a2f3d27602115516ab14";
			rootfs = "038-0955-007.dmg";
			rootfs_key = "f607711d4db94bba7a4866f095aed082c8485bfbcd0f411f1e65158f585915edd5cfeec1";
			rootfs_size = "800";
		} else if (xml_ipsw_md5 == "694c93b5b608513136ba8956dff28ba7") {
			//iOS 4.3.1/8G4 -- iPhone 3GS
			iDevice = "iPhone 3GS";
			iBSSIV = "086c8a91cbda60d094e5f96c8a62c9bd";
			iBSSKey = "795b5196dfcbe8aea9803154148c087d59a1659a0543c59d4bf7e2ff889b7ed7";
			Ramdisk = "038-0900-005.dmg";
			RamdiskIV = "";
			RamdiskKey = "";
			rootfs = "038-0935-003.dmg";
			rootfs_key = "c338fb2858bd5dad4cfb073d4fab2fbed4a3f2d1541bc50d8443f3b18475cd1b62c25983";
			rootfs_size = "850";
		} else if (xml_ipsw_md5 == "32f9a71430c4dd025adab3b73d4a5242") {
			//iOS 4.3.1/8G4 -- iPhone 4
			iDevice = "iPhone 4";
			iBSSIV = "a441763f051b5537aeefefedf3cf52c9";
			iBSSKey = "bbf0968d5799f444aae52bdf9a2f9ae26d30d94e8de1f9867fba82da220fc852";
			Ramdisk = "038-0902-005.dmg";
			RamdiskIV = "";
			RamdiskKey = "";
			rootfs = "038-0937-003.dmg";
			rootfs_key = "f6331068497fa4741e135329c399f69b3c109854835789cc6f23f759f333f5e7bbfcdde7";
			rootfs_size = "900";
		} else if (xml_ipsw_md5 == "47827ca8d127f28663d5b70b0784236e") {
			//iOS 4.3.1/8G4 -- iPod Touch 3G
			iDevice = "iPod Touch 3G";
			iBSSIV = "24bb9af0d82d9c22f864dcd408038d20";
			iBSSKey = "1cacc4990ee4eca8a8ab800b00ab0951fd51c2b90bc8845c55101ce78665b52d";
			Ramdisk = "038-0901-005.dmg";
			RamdiskIV = "";
			RamdiskKey = "";
			rootfs = "038-0939-003.dmg";
			rootfs_key = "f466f8ab4e9accb91ed1098ebda56b27b5dd06ddc62485394a53eb77bf190afd80274c02";
			rootfs_size = "800";
		} else if (xml_ipsw_md5 == "b0e356267a1407e4d7a7b0f48a07c5c2") {
			//iOS 4.3.1/8G4 -- iPod Touch 4
			iDevice = "iPod Touch 4";
			iBSSIV = "28893e051476c5b321751a0f8ee7cff7";
			iBSSKey = "826789656e0cf85bdc280b8e39490388c32b6c39004283624a6f3a6c69adef31";
			Ramdisk = "038-0902-005.dmg";
			RamdiskIV = "";
			RamdiskKey = "";
			rootfs = "038-0886-005.dmg";
			rootfs_key = "2cce34479eeb3701b3888f81c0465d2d98133af3a2761d0a82ad5074ca8efc1c5593eccb";
			rootfs_size = "900";
		} else if (xml_ipsw_md5 == "fe4f80f8ff2fa298559b392b64e84bb8") {
			//iOS 4.3.1/8G4 -- iPad 1G
			iDevice = "iPad 1G";
			iBSSIV = "815f744558bb65d991dd1dd2502301b2";
			iBSSKey = "99f0194451de1ac1eb4a254dd843f89a9cae2537b130793bff2c5e16a5b2b851";
			Ramdisk = "038-0902-005.dmg";
			RamdiskIV = "";
			RamdiskKey = "";
			rootfs = "038-0936-003.dmg";
			rootfs_key = "c309657d0abe1b66b4be046bb4b03fb540741f9cbc1e49951cf21e11332d9b0b66afd31e";
			rootfs_size = "750";
		} else if (xml_ipsw_md5 == "7c1c714f24a89c2f2c71e26d37cde3f0") {
			//iOS 4.3.2/8H7 -- iPhone 3GS
			iDevice = "iPhone 3GS";
			iBSSIV = "181ee0b292538b2052dbd119770d7348";
			iBSSKey = "f131471b406795abf7ae70617dd30968e5ad6b847b84a1671d5801c04fc07643";
			Ramdisk = "038-1033-007.dmg";
			RamdiskIV = "";
			RamdiskKey = "";
			rootfs = "038-1022-007.dmg";
			rootfs_key = "69a370c1b64b35f692e87e866bcd460a98a10c56ed05055eb7c675f101894ea504f7bc46";
			rootfs_size = "800";
		} else if (xml_ipsw_md5 == "8cb3a9964a2a99414030f662d3009deb") {
			//iOS 4.3.2/8H7 -- iPhone 4
			iDevice = "iPhone 4";
			iBSSIV = "cdd50b45ca1bac4f718d9eb23ce9f0a8";
			iBSSKey = "8ef00005aa2c01ae409d55e330171589af79d76ac86639e76003835d5d82ffc4";
			Ramdisk = "038-1035-007.dmg";
			RamdiskIV = "";
			RamdiskKey = "";
			rootfs = "038-1025-007.dmg";
			rootfs_key = "30804cac61ba4df96999aa4e1ea3a2a18bfbe875534a66a0bb1add095e307a19a7176c82";
			rootfs_size = "900";
		} else if (xml_ipsw_md5 == "7f831b30d33f80c7f92442cb041227ab") {
			//iOS 4.3.2/8H7 -- iPod Touch 3G
			iDevice = "iPod Touch 3G";
			iBSSIV = "a4ff016010ce6831cae2a7009113c16f";
			iBSSKey = "f7e6576f69cbcefbe96939efa39600cada2e13d12a6372499eff67f0566b86d2";
			Ramdisk = "038-1034-007.dmg";
			RamdiskIV = "";
			RamdiskKey = "";
			rootfs = "038-1040-007.dmg";
			rootfs_key = "7085a2976bd57eceedcbbe88a270e1a5028133c288b8afb122c2f886830a9a641daf8bd4";
			rootfs_size = "900";
		} else if (xml_ipsw_md5 == "4a002a4596a681efd9cdbf6f2fd72e74") {
			//iOS 4.3.2/8H7 -- iPod Touch 4
			iDevice = "iPod Touch 4";
			iBSSIV = "682684c4247740ca26c6823e58a36cdf";
			iBSSKey = "ac17ad4e4e65d5d988b28bde260ade08a7b3f284a22b03a386b53dd761b4ccb5";
			Ramdisk = "038-1035-007.dmg";
			RamdiskIV = "";
			RamdiskKey = "";
			rootfs = "038-1023-007.dmg";
			rootfs_key = "401b22ae26cca1aa2e119c17a6c389a1ba6aea0fbff4912000a77df953f010637b35d0ee";
			rootfs_size = "900";
		} else if (xml_ipsw_md5 == "24027c4381a6cdfdd8a03a17177d1d6c") {
			//iOS 4.3.2/8H7 -- iPad 1G
			iDevice = "iPad 1G";
			iBSSIV = "de7258db01e653c6d8ffe0ab18a5a5c7";
			iBSSKey = "6d0afca5d390a7e48399cd534c8fd7db5a3f8fd4361631f6dffca75fbafca6aa";
			Ramdisk = "038-1035-007.dmg";
			RamdiskIV = "";
			RamdiskKey = "";
			rootfs = "038-1024-007.dmg";
			rootfs_key = "25c0b2a27afd23b9ddc9555a28ba8e77548e62d9e2ef56700bc40d22b2c50416aee9313c";
			rootfs_size = "750";
		} else if (xml_ipsw_md5 == "d9a02961311ffac8197e8db3b48e449d") {
			//iOS 4.3.3/8J2 -- iPhone 3GS
			iDevice = "iPhone 3GS";
			iBSSIV = "181ee0b292538b2052dbd119770d7348";
			iBSSKey = "f131471b406795abf7ae70617dd30968e5ad6b847b84a1671d5801c04fc07643";
			Ramdisk = "038-1447-003.dmg";
			RamdiskIV = "";
			RamdiskKey = "";
			rootfs = "038-1417-003.dmg";
			rootfs_key = "148f4fca734e973551fc8fa65a04883041854b060e3fe1e6c3ca4499a3204d1d97594a47";
			rootfs_size = "850";
		} else if (xml_ipsw_md5 == "a0cb7313c5535991d62890c7eef60f9a") {
			//iOS 4.3.3/8J2 -- iPhone 4
			iDevice = "iPhone 4";
			iBSSIV = "cdd50b45ca1bac4f718d9eb23ce9f0a8";
			iBSSKey = "8ef00005aa2c01ae409d55e330171589af79d76ac86639e76003835d5d82ffc4";
			Ramdisk = "038-1449-003.dmg";
			RamdiskIV = "";
			RamdiskKey = "";
			rootfs = "038-1423-003.dmg";
			rootfs_key = "246f17ec6660672b3207ece257938704944a83601205736409b61fc3565512559abd0f82";
			rootfs_size = "900";
		} else if (xml_ipsw_md5 == "7c8d3ccaccd1573dc31d6de555b987f9") {
			//iOS 4.3.3/8J2 -- iPod Touch 3G
			iDevice = "iPod Touch 3G";
			iBSSIV = "a4ff016010ce6831cae2a7009113c16f";
			iBSSKey = "f7e6576f69cbcefbe96939efa39600cada2e13d12a6372499eff67f0566b86d2";
			Ramdisk = "038-1448-003.dmg";
			RamdiskIV = "";
			RamdiskKey = "";
			rootfs = "038-1491-003.dmg";
			rootfs_key = "affbe8f884694f4a3848097fa22a71bc1de24b070aa7e79f58a0880602dd21444cd559f2";
			rootfs_size = "900";
		} else if (xml_ipsw_md5 == "dd5003cc00dbaa9fbf0182c5a2e5d6ed") {
			//iOS 4.3.3/8J2 -- iPod Touch 4
			iDevice = "iPod Touch 4";
			iBSSIV = "682684c4247740ca26c6823e58a36cdf";
			iBSSKey = "ac17ad4e4e65d5d988b28bde260ade08a7b3f284a22b03a386b53dd761b4ccb5";
			Ramdisk = "038-1449-003.dmg";
			RamdiskIV = "";
			RamdiskKey = "";
			rootfs = "038-1419-003.dmg";
			rootfs_key = "d2877c05eb72e55d52d4e5e71c523a503c5bb8c85f6c7077d821140beea967782d30858d";
			rootfs_size = "930";
		} else if (xml_ipsw_md5 == "d20493bb1ba0450f2ee01d081ba8eb27") {
			//iOS 4.3.3/8J3 -- iPad 1G
			iDevice = "iPad 1G";
			iBSSIV = "de7258db01e653c6d8ffe0ab18a5a5c7";
			iBSSKey = "6d0afca5d390a7e48399cd534c8fd7db5a3f8fd4361631f6dffca75fbafca6aa";
			Ramdisk = "038-1449-004.dmg";
			RamdiskIV = "";
			RamdiskKey = "";
			rootfs = "038-1421-004.dmg";
			rootfs_key = "765d0fecc4f714ca20fa6eceeabb454b04cd2998cc3ab3bba290866788a8c6cf555945ac";
			rootfs_size = "800";
		} else if (xml_ipsw_md5 == "#FUTURE#") {
			//iOS x.x/XxX -- i
			iDevice = "";
			iBSSIV = "";
			iBSSKey = "";
			Ramdisk = "";
			RamdiskIV = "";
			RamdiskKey = "";
			rootfs = "";
			rootfs_key = "";
			rootfs_size = "";
		}
	}
}
