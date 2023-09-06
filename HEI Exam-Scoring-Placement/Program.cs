namespace HEI_Exam_Scoring_Placement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string file_candidates = @"candidates.txt";
            string file_departments = @"departments.txt";

            //Dosyalardan verileri çekme
            List<string> cand_lines = File.ReadAllLines(file_candidates).ToList();
            List<string> dep_lines = File.ReadAllLines(file_departments).ToList();

            string[] key = { "A", "B", "D", "C", "C", "C", "A", "D", "B", "C", "D", "B", "A", "C", "B", "A", "C", "D", "C", "D", "A", "D", "B", "C", "D" };
            int cand_lines_num = 0, dep_lines_num = 0, cand_input_line_num = 31;

            foreach (var inputs in cand_lines)
            {
                cand_lines_num++;
                if (cand_lines_num > 40)
                {
                    Console.WriteLine("You have entered too much candidate data. Run the program again and enter less candidate data.");
                    Console.ReadLine();
                    return;
                }
            }
            foreach (var inputs in dep_lines)
            {
                dep_lines_num++;
                if (dep_lines_num > 10)
                {
                    Console.WriteLine("You have entered too much department data. Run the program again and enter less departmen data.");
                    Console.ReadLine();
                    return;
                }
            }


            int cand_line_num = 0, dep_line_num = 0, cand_line_input_num = 31, dep_line_input_num = 3;
            string[,] candidates = new string[cand_lines_num, cand_input_line_num];
            string[,] departments = new string[10, 8];
            string[,] dep_placement = new string[10, 2];
            int[,] grades = new int[cand_lines_num, 3];

            //Verileri 2 boyutları arraye çevirme 

            foreach (var inputs in cand_lines)
            {
                string[] input_cand = inputs.Split(',');

                for (int i = 0; i < cand_line_input_num; i++)
                {
                    candidates[cand_line_num, i] = input_cand[i];
                }
                cand_line_num++;
            }


            foreach (var inputs in dep_lines)
            {
                string[] input_dep = inputs.Split(',');

                for (int j = 0; j < dep_line_input_num; j++)
                {
                    departments[dep_line_num, j] = input_dep[j];
                }
                dep_line_num++;
            }


            //Puan hesaplaması
            for (int no = 0; no < cand_lines_num; no++)
            {
                int grade = 0;
                for (int answer = 6; answer < cand_line_input_num; answer++)
                {
                    if (candidates[no, answer] == key[answer - 6])
                    {
                        grade += 4;
                    }
                    else if (candidates[no, answer] == " ") { grade += 0; }
                    else { grade -= 3; }
                }
                if (grade < 0)
                {
                    grade = 0;
                }
                grades[no, 0] = Convert.ToInt32(candidates[no, 0]);
                grades[no, 1] = grade;
                grades[no, 2] = no + 1;
            }
            //Yerleştirme Arraylerinin Belirlenmesi ve Atanması

            for (int array_no = 0; array_no < dep_lines_num; array_no++)
            {
                if (departments[array_no, 2] == null) { break; }
                string department = departments[array_no, 0];
                string quota = departments[array_no, 2];
                if (Convert.ToInt32(quota) > 8)
                {
                    Console.WriteLine("You have entered too much quota data. Run the program again and enter less quota data.");
                    Console.ReadLine();
                    return;
                }
                dep_placement[array_no, 0] = department;
                dep_placement[array_no, 1] = quota;
            }

            for (int empty = dep_lines_num; empty < 10; empty++)
            {
                string department = "0";
                string quota = "0";
                dep_placement[empty, 0] = department;
                dep_placement[empty, 1] = quota;
            }

            string[] D1_array = new string[Convert.ToInt16(dep_placement[0, 1])];
            string[] D2_array = new string[Convert.ToInt16(dep_placement[1, 1])];
            string[] D3_array = new string[Convert.ToInt16(dep_placement[2, 1])];
            string[] D4_array = new string[Convert.ToInt16(dep_placement[3, 1])];
            string[] D5_array = new string[Convert.ToInt16(dep_placement[4, 1])];
            string[] D6_array = new string[Convert.ToInt16(dep_placement[5, 1])];
            string[] D7_array = new string[Convert.ToInt16(dep_placement[6, 1])];
            string[] D8_array = new string[Convert.ToInt16(dep_placement[7, 1])];
            string[] D9_array = new string[Convert.ToInt16(dep_placement[8, 1])];
            string[] D10_array = new string[Convert.ToInt16(dep_placement[9, 1])];



            //Öğrencilerin Sıralanması
            int temp = 0;
            int temp_2 = 0;
            for (int i = 0; i < grades.GetLength(0) - 1; i++)
            {
                for (int j = i + 1; j < grades.GetLength(0); j++)
                {
                    if (grades[i, 1] < grades[j, 1])
                    {
                        temp = grades[i, 1];
                        grades[i, 1] = grades[j, 1];
                        grades[j, 1] = temp;
                        temp_2 = grades[i, 0];
                        grades[i, 0] = grades[j, 0];
                        grades[j, 0] = temp_2;


                    }
                    else if (grades[i, 1] == grades[j, 1])
                    {
                        if (Convert.ToDouble(candidates[i, 2]) < Convert.ToDouble(candidates[j, 2]))
                        {
                            temp = grades[i, 1];
                            grades[i, 1] = grades[j, 1];
                            grades[j, 1] = temp;
                            temp_2 = grades[i, 0];
                            grades[i, 0] = grades[j, 0];
                            grades[j, 0] = temp_2;
                        }
                    }
                }
            }

            for (int m = 0; m < grades.GetLength(0); m++)
            {
                grades[m, 2] = m + 1;
            }

            //Öğrencilerin Yerleşimi

            int min_pass_score = 40;
            string dep_choice_1 = " ";
            string dep_choice_2 = " ";
            string dep_choice_3 = " ";
            int D1 = 0, D2 = 0, D3 = 0, D4 = 0, D5 = 0, D6 = 0, D7 = 0, D8 = 0, D9 = 0, D10 = 0;

            for (int st_n = 0; st_n < grades.GetLength(0); st_n++)
            {
                if (grades[st_n, 1] < min_pass_score) { continue; }
                else
                {

                    for (int st_1 = 0; st_1 < grades.GetLength(0); st_1++)
                    {
                        if (candidates[st_1, 0] == Convert.ToString(grades[st_n, 0]))
                        {
                            dep_choice_1 = candidates[st_1, 3];
                            dep_choice_2 = candidates[st_1, 4];
                            dep_choice_3 = candidates[st_1, 5];

                            if (dep_choice_1 == "D1" && dep_placement[0, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[0, 1]) > 0)
                                {
                                    D1_array[D1] = candidates[st_1, 0];
                                    D1 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[0, 1]) - 1;
                                    dep_placement[0, 1] = Convert.ToString(term_quota);
                                }
                            }
                            else if (dep_choice_1 == "D2" && dep_placement[1, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[1, 1]) > 0)
                                {
                                    D2_array[D2] = candidates[st_1, 0];
                                    D2 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[1, 1]) - 1;
                                    dep_placement[1, 1] = Convert.ToString(term_quota);
                                }

                            }
                            else if (dep_choice_1 == "D3" && dep_placement[2, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[2, 1]) > 0)
                                {
                                    D3_array[D3] = candidates[st_1, 0];
                                    D3 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[2, 1]) - 1;
                                    dep_placement[2, 1] = Convert.ToString(term_quota);
                                }

                            }

                            else if (dep_choice_1 == "D4" && dep_placement[3, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[3, 1]) > 0)
                                {
                                    D4_array[D4] = candidates[st_1, 0];
                                    D4 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[3, 1]) - 1;
                                    dep_placement[3, 1] = Convert.ToString(term_quota);
                                }
                            }
                            else if (dep_choice_1 == "D5" && dep_placement[4, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[4, 1]) > 0)
                                {
                                    D5_array[D5] = candidates[st_1, 0];
                                    D5 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[4, 1]) - 1;
                                    dep_placement[4, 1] = Convert.ToString(term_quota);
                                }
                            }
                            else if (dep_choice_1 == "D6" && dep_placement[5, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[5, 1]) > 0)
                                {
                                    D6_array[D6] = candidates[st_1, 0];
                                    D6 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[5, 1]) - 1;
                                    dep_placement[5, 1] = Convert.ToString(term_quota);
                                }
                            }
                            else if (dep_choice_1 == "D7" && dep_placement[6, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[6, 1]) > 0)
                                {
                                    D7_array[D7] = candidates[st_1, 0];
                                    D7 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[6, 1]) - 1;
                                    dep_placement[6, 1] = Convert.ToString(term_quota);
                                }
                            }
                            else if (dep_choice_1 == "D8" && dep_placement[7, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[7, 1]) > 0)
                                {
                                    D8_array[D8] = candidates[st_1, 0];
                                    D8 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[7, 1]) - 1;
                                    dep_placement[7, 1] = Convert.ToString(term_quota);
                                }
                            }
                            else if (dep_choice_1 == "D9" && dep_placement[8, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[8, 1]) > 0)
                                {
                                    D9_array[D9] = candidates[st_1, 0];
                                    D9 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[8, 1]) - 1;
                                    dep_placement[8, 1] = Convert.ToString(term_quota);
                                }
                            }
                            else if (dep_choice_1 == "D10" && dep_placement[9, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[9, 1]) > 0)
                                {
                                    D10_array[D10] = candidates[st_1, 0];
                                    D10 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[9, 1]) - 1;
                                    dep_placement[9, 1] = Convert.ToString(term_quota);
                                }
                            }
                            else if (dep_choice_2 == "D1" && dep_placement[0, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[0, 1]) > 0)
                                {
                                    D1_array[D1] = candidates[st_1, 0];
                                    D1 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[0, 1]) - 1;
                                    dep_placement[0, 1] = Convert.ToString(term_quota);
                                }
                            }
                            else if (dep_choice_2 == "D2" && dep_placement[1, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[1, 1]) > 0)
                                {
                                    D2_array[D2] = candidates[st_1, 0];
                                    D2 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[1, 1]) - 1;
                                    dep_placement[1, 1] = Convert.ToString(term_quota);
                                }

                            }
                            else if (dep_choice_2 == "D3" && dep_placement[2, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[2, 1]) > 0)
                                {
                                    D3_array[D3] = candidates[st_1, 0];
                                    D3 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[2, 1]) - 1;
                                    dep_placement[2, 1] = Convert.ToString(term_quota);
                                }

                            }

                            else if (dep_choice_2 == "D4" && dep_placement[3, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[3, 1]) > 0)
                                {
                                    D4_array[D4] = candidates[st_1, 0];
                                    D4 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[3, 1]) - 1;
                                    dep_placement[3, 1] = Convert.ToString(term_quota);
                                }
                            }
                            else if (dep_choice_2 == "D5" && dep_placement[4, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[4, 1]) > 0)
                                {
                                    D5_array[D5] = candidates[st_1, 0];
                                    D5 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[4, 1]) - 1;
                                    dep_placement[4, 1] = Convert.ToString(term_quota);
                                }
                            }
                            else if (dep_choice_2 == "D6" && dep_placement[5, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[5, 1]) > 0)
                                {
                                    D6_array[D6] = candidates[st_1, 0];
                                    D6 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[5, 1]) - 1;
                                    dep_placement[5, 1] = Convert.ToString(term_quota);
                                }
                            }

                            else if (dep_choice_2 == "D7" && dep_placement[6, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[6, 1]) > 0)
                                {
                                    D7_array[D7] = candidates[st_1, 0];
                                    D7 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[6, 1]) - 1;
                                    dep_placement[6, 1] = Convert.ToString(term_quota);
                                }
                            }
                            else if (dep_choice_2 == "D8" && dep_placement[7, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[7, 1]) > 0)
                                {
                                    D8_array[D8] = candidates[st_1, 0];
                                    D8 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[7, 1]) - 1;
                                    dep_placement[7, 1] = Convert.ToString(term_quota);
                                }
                            }
                            else if (dep_choice_2 == "D9" && dep_placement[8, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[8, 1]) > 0)
                                {
                                    D9_array[D9] = candidates[st_1, 0];
                                    D9 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[8, 1]) - 1;
                                    dep_placement[8, 1] = Convert.ToString(term_quota);
                                }
                            }
                            else if (dep_choice_2 == "D10" && dep_placement[9, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[9, 1]) > 0)
                                {
                                    D10_array[D10] = candidates[st_1, 0];
                                    D10 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[9, 1]) - 1;
                                    dep_placement[9, 1] = Convert.ToString(term_quota);
                                }
                            }
                            else if (dep_choice_3 == "D1" && dep_placement[0, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[0, 1]) > 0)
                                {
                                    D1_array[D1] = candidates[st_1, 0];
                                    D1 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[0, 1]) - 1;
                                    dep_placement[0, 1] = Convert.ToString(term_quota);
                                }
                            }
                            else if (dep_choice_3 == "D2" && dep_placement[1, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[1, 1]) > 0)
                                {
                                    D2_array[D2] = candidates[st_1, 0];
                                    D2 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[1, 1]) - 1;
                                    dep_placement[1, 1] = Convert.ToString(term_quota);
                                }

                            }
                            else if (dep_choice_3 == "D3" && dep_placement[2, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[2, 1]) > 0)
                                {
                                    D3_array[D3] = candidates[st_1, 0];
                                    D3 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[2, 1]) - 1;
                                    dep_placement[2, 1] = Convert.ToString(term_quota);
                                }

                            }

                            else if (dep_choice_3 == "D4" && dep_placement[3, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[3, 1]) > 0)
                                {
                                    D4_array[D4] = candidates[st_1, 0];
                                    D4 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[3, 1]) - 1;
                                    dep_placement[3, 1] = Convert.ToString(term_quota);
                                }
                            }
                            else if (dep_choice_3 == "D5" && dep_placement[4, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[4, 1]) > 0)
                                {
                                    D5_array[D5] = candidates[st_1, 0];
                                    D5 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[4, 1]) - 1;
                                    dep_placement[4, 1] = Convert.ToString(term_quota);
                                }
                            }
                            else if (dep_choice_3 == "D6" && dep_placement[5, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[5, 1]) > 0)
                                {
                                    D6_array[D6] = candidates[st_1, 0];
                                    D6 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[5, 1]) - 1;
                                    dep_placement[5, 1] = Convert.ToString(term_quota);
                                }
                            }

                            else if (dep_choice_3 == "D7" && dep_placement[6, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[6, 1]) > 0)
                                {
                                    D7_array[D7] = candidates[st_1, 0];
                                    D7 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[6, 1]) - 1;
                                    dep_placement[6, 1] = Convert.ToString(term_quota);
                                }
                            }
                            else if (dep_choice_3 == "D8" && dep_placement[7, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[7, 1]) > 0)
                                {
                                    D8_array[D8] = candidates[st_1, 0];
                                    D8 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[7, 1]) - 1;
                                    dep_placement[7, 1] = Convert.ToString(term_quota);
                                }
                            }
                            else if (dep_choice_3 == "D9" && dep_placement[8, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[8, 1]) > 0)
                                {
                                    D9_array[D9] = candidates[st_1, 0];
                                    D9 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[8, 1]) - 1;
                                    dep_placement[8, 1] = Convert.ToString(term_quota);
                                }
                            }
                            else if (dep_choice_3 == "D10" && dep_placement[7, 1] != "0")
                            {
                                if (Convert.ToInt32(dep_placement[9, 1]) > 0)
                                {
                                    D10_array[D10] = candidates[st_1, 0];
                                    D10 += 1;
                                    int term_quota = Convert.ToInt32(dep_placement[9, 1]) - 1;
                                    dep_placement[9, 1] = Convert.ToString(term_quota);
                                }
                            }

                        }
                    }

                }
            }


            //Verilerin Ekrana Bastırılması
            Console.WriteLine("NUMBER    NAME-SURNAME     GRADE ");
            for (int st_n = 0; st_n < grades.GetLength(0); st_n++)
            {
                for (int st_1 = 0; st_1 < grades.GetLength(0); st_1++)
                {
                    if (candidates[st_1, 0] == Convert.ToString(grades[st_n, 0]))
                    {
                        Console.WriteLine(candidates[st_1, 0] + "  " + candidates[st_1, 1] + "  " + grades[st_n, 1]);

                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("NO    DEPARTMENTS    STUDENTS");

            if (departments[0, 0] != null || departments[0, 1] != null)
                Console.Write(departments[0, 0] + "   " + departments[0, 1] + "   ");
            for (int ar = 0; ar < D1_array.Length; ar++)
            {
                if (ar == D1_array.Length - 1)
                {
                    Console.WriteLine(D1_array[ar]);
                }
                else
                {
                    Console.Write(D1_array[ar] + " ");
                }

            }

            if (departments[1, 0] != null || departments[1, 1] != null)
                Console.Write(departments[1, 0] + "   " + departments[1, 1] + "   ");
            for (int ar = 0; ar < D2_array.Length; ar++)
            {
                if (ar == D2_array.Length - 1)
                {
                    Console.WriteLine(D2_array[ar]);
                }
                else
                {
                    Console.Write(D2_array[ar] + " ");
                }
            }

            if (departments[2, 0] != null || departments[2, 1] != null)
                Console.Write(departments[2, 0] + "   " + departments[2, 1] + "   ");
            for (int ar = 0; ar < D3_array.Length; ar++)
            {
                if (ar == D3_array.Length - 1)
                {
                    Console.WriteLine(D3_array[ar]);
                }
                else
                {
                    Console.Write(D3_array[ar] + " ");
                }
            }

            if (departments[3, 0] != null || departments[3, 1] != null)
                Console.Write(departments[3, 0] + "   " + departments[3, 1] + "   ");
            for (int ar = 0; ar < D4_array.Length; ar++)
            {
                if (ar == D4_array.Length - 1)
                {
                    Console.WriteLine(D4_array[ar]);
                }
                else
                {
                    Console.Write(D4_array[ar] + " ");
                }
            }

            if (departments[4, 0] != null || departments[4, 1] != null)
                Console.Write(departments[4, 0] + "   " + departments[4, 1] + "   ");
            for (int ar = 0; ar < D5_array.Length; ar++)
            {
                if (ar == D5_array.Length - 1)
                {
                    Console.WriteLine(D5_array[ar]);
                }
                else
                {
                    Console.Write(D5_array[ar] + " ");
                }
            }
            if (departments[5, 0] != null || departments[5, 1] != null)
                Console.Write(departments[5, 0] + "   " + departments[5, 1] + "   ");
            for (int ar = 0; ar < D6_array.Length; ar++)
            {
                if (ar == D6_array.Length - 1)
                {
                    Console.WriteLine(D6_array[ar]);
                }
                else
                {
                    Console.Write(D6_array[ar] + " ");
                }
            }
            if (departments[6, 0] != null || departments[6, 1] != null)
                Console.Write(departments[6, 0] + "   " + departments[6, 1] + "   ");
            for (int ar = 0; ar < D7_array.Length; ar++)
            {
                if (ar == D7_array.Length - 1)
                {
                    Console.WriteLine(D7_array[ar]);
                }
                else
                {
                    Console.Write(D7_array[ar] + " ");
                }
            }
            if (departments[7, 0] != null || departments[7, 1] != null)
                Console.Write(departments[7, 0] + "   " + departments[7, 1] + "   ");
            for (int ar = 0; ar < D8_array.Length; ar++)
            {
                if (ar == D8_array.Length - 1)
                {
                    Console.WriteLine(D8_array[ar]);
                }
                else
                {
                    Console.Write(D8_array[ar] + " ");
                }
            }
            if (departments[8, 0] != null || departments[8, 1] != null)
                Console.Write(departments[8, 0] + "   " + departments[8, 1] + "   ");
            for (int ar = 0; ar < D9_array.Length; ar++)
            {
                if (ar == D9_array.Length - 1)
                {
                    Console.WriteLine(D9_array[ar]);
                }
                else
                {
                    Console.Write(D9_array[ar] + " ");
                }
            }
            if (departments[9, 0] != null || departments[9, 1] != null)
            {
                Console.Write(departments[9, 0] + "   " + departments[9, 1] + " ");
            }
            for (int ar = 0; ar < D10_array.Length; ar++)
            {
                if (ar == D10_array.Length - 1)
                {
                    Console.WriteLine(D10_array[ar]);
                }
                else
                {
                    Console.Write(D10_array[ar] + " ");
                }
            }
            Console.ReadLine();

        }
    }
}