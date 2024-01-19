using App;

namespace Tests
{
    // ��������� ��������� ����� ������� �������� ������ ������
    // ������� ������ (�����) �������� � ������ (����) ����� ...
    [TestClass]
    public class StringHelperTest
    {
        // ... ������� ������ - �������� ����� (��� ������� ������)
        [TestMethod]
        public void EllipsisTest()
        {
            StringHelper stringHelper = new();
            Assert.IsNotNull(stringHelper, "New StringHelper should be Non-Null");
            Assert.AreEqual(                          // ���� � ������������� ��������� -
                "Hello...",                           // ���������� ������. �������� -
                stringHelper                          // ��������� ��������, ���� - 
                    .Ellipsis("Hello, World!", 5),    // ��������.
                "'Hello, World!' -> 'Hello...'"       // ����������� - ����������� ���
            );                                        // ������ �����
            // ����� ��� ������ ����� �����, ��� ���� �����������
            // �������� ��������, ���� ���� �. �������� ���� �������
            // ����������� ��������� - ������� �����.
            // ���������, ��� ������ ����, return "Hello..." ���� ��������� ����
            Assert.AreEqual(
                "����� ��� ����...",
                stringHelper
                    .Ellipsis("����� ��� ������ ����� �����", 15) );
            // return max==5 ? "Hello..." : "����� ��� ����..."

        }

        [TestMethod]
        public void SpacefyTest()
        {
            StringHelper stringHelper = new();
            Assert.IsNotNull(stringHelper, "New StringHelper should be Non-Null");
            // 1. ���� ���������� �� ����, �� ����������� �����
            Assert.AreEqual("1 000", stringHelper.Spacefy(1000));
            // 3. ��� �������� ������� �������� ������ ��� ������
            // ������ ���� ���� �������������� (���. �. 2.2 � �����)
            // - ���������� ���� �����
            Dictionary<double, String> testCases = new()
            {
                { 2, "2" },
                { 20, "20" },
                { 200, "200" },
                { 2000, "2 000" },
                { 750387326, "750 387 326" },
                { 3750387326, "3 750 387 326" },  // �� ������� ����������� ��� <int> (�� uint)
                { -3750387326, "-3 750 387 326" },  // �� ������� ����������� �� long
                { 1000.1, "1 000.1" },  // 5. �� ������� ����������� �� double
                { 1000.123, "1 000.123" },
                { 1000.1234, "1 000.123 4" },
                { 1000.12345, "1 000.123 45" },
                { 1000.123456, "1 000.123 456" },
                { 0.1, "0.1" },  
                { 0.01, "0.01" },
                { 0.001, "0.001" },
                { 0.0001, "0.000 1" },
                { 0.00001, "0.000 01" },
                { 0, "0" },
                { 1.0, "1" },
            };
            foreach( var testCase in testCases )
            {
                Assert.AreEqual(
                    testCase.Value,
                    stringHelper.Spacefy(testCase.Key),
                    $"Spacefy({testCase.Key})"
                );
            }
        }


        [TestMethod]
        public void EllipsisErrorsTest()
        {
            StringHelper stringHelper = new();
            Assert.IsNotNull(stringHelper, "New StringHelper should be Non-Null");
            
            var ex = Assert.ThrowsException<ArgumentNullException>(
                () => stringHelper.Ellipsis(null!),
                "Ellipsis(null!) --> Exception"
            );
            // ��������, ��� ����������� ������� ������ ����� ��������� "input"
            Assert.IsTrue(
                ex.Message.Contains("input"),
                $"ArgumentNullException must contain 'input' msg='{ex.Message}' " 
            );
            // �� ��������� Assert.NotThrows ��������� ��� �����
            // �� ��������� Assert, ����� ���������� ��������� ����
            stringHelper.Ellipsis("123");
            // ��� ����� ����������� try-catch
            try
            {
                stringHelper.Ellipsis("123", 20);
            }
            catch
            {
                Assert.Fail("Ellipsis('123', 20) must NOT throw exception ");
            }
            // �������� ������� - String.Empty
            Assert.IsNotNull(
                stringHelper.Ellipsis(String.Empty)
            );  // �� ���� ������ ��� Assert.NotThrows
        }

    }
}
/* �.�. UrlCombine
 * - ���������� �������� ��������� �� null
 *  = ���� ������ null - ������� � ����-����� �������
 *  = ���� ������ null, �� �� ���������� ��� ����������� �������
 * - ���������� �������� �� String.Empty 
 *  = ���� ������ ������ - ��������� String.Empty
 *  = ���� ������ �� �������, ������ ������� - ���������
 *  = ������ �������, ������ � - �������
 * * ����������� ����� UrlCombine �� ������� ������� �������
 *    ��������� ������ (���������). ����������� �������� �� ���������, �� 
 *    ������ (� �.�. null) �������� ������������ ����� ���� ��� 
 *    �� ���� ���� ���������, � ����� �� ����� �� ������ 
 *    ��������
 */
