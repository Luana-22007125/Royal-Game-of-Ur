using System.Net.Mime;
using System;
using System.Collections.Generic;

namespace OlaMundo
{
    class Program
    {
        static void Main(string[] args)
        {
            Player p1 = new Player();
            Player p2 = new Player();

            /*
            O tabuleiro criado em ASCII:
            Console.WriteLine("*-*");
            Console.WriteLine("---");
            Console.WriteLine("---");
            Console.WriteLine("-*-");
            Console.WriteLine(" - ");
            Console.WriteLine(" - ");
            Console.WriteLine("*-*");
            Console.WriteLine("---"); */

            Boolean play = true;
            Console.WriteLine("Bem vindo, por favor pressione qualquer tecla para começar o jogo, e boa sorte!");
            Console.ReadLine();
            int round = 2;

            do {
                //Limpa o espaço na consola
                for (int i = 0; i<20;i++) {
                    Console.WriteLine("");
                }

                //Se os jogadores acabarem as peças, ganham
                if(p1.hasAllFinished()) {
                    play = false;
                    Console.WriteLine("Jogador 1 ganhou!");
                    break;
                }
                if(p2.hasAllFinished()) {
                    play = false;
                    Console.WriteLine("Jogador 2 ganhou!");
                    break;
                }
                showMap(p1, p2);
                if (round == 1) {
                    round = 2;
                } else {
                    round = 1;
                }

                //O jogador rola os dados
                Console.WriteLine("");
                Console.WriteLine("Jogador " + round + ": pressione qualquer tecla para rolar os dados.");
                Console.ReadLine();
                Random random = new Random();
                int total = 0;
                int[] all = new int[4]; //4 dados
                string pool = "";
                for (int i = 0; i<4; i++) {
                    all[i] = random.Next(0, 3);
                    if (all[i] == 1) total += all[i];
                    pool += numbersToChars(all[i]);
                }

                //Caso role 0
                Console.WriteLine(pool);
                if (total == 0) {
                    Console.WriteLine("Jogador " + round + ": teve azar, rolou 0.");
                    Console.WriteLine("> Pressione qualquer tecla para continuar.");
                    Console.ReadLine();
                } else {

                    //A não ser que o jogador role 0,
                    int slot;
                    Boolean again = true;
                    while (again) {
                        Console.WriteLine("Jogador " + round + ": Com que peça quer andar " + total + " casa(s)?");
                        string resposta = Console.ReadLine();
                        if (resposta.Equals("e")) {
                            play = false;
                            break;
                        }
                        slot = Int16.Parse(resposta);
                        if (slot < 0 || slot > 15) slot = 0;

                        //Casos de para onde as peças vão, se podem ou não ir
                        //Ronda 1 ou 2 conforme o jogador
                        if (round == 1) {
                            if (p1.slotHasPiece(slot) && (slot + total <= 15)) {
                                int newslot = slot + total;
                                if (newslot == 8 && p2.slotHasPiece(8)) {
                                    Console.WriteLine("Este lugar está ocupado.");
                                } else {
                                    if (newslot > 4 && newslot < 8 || newslot > 8 && newslot < 13) {
                                        if (p2.slotHasPiece(newslot)) {
                                            p2.setPieceNewSlot(newslot, -newslot);
                                            p1.setPieceNewSlot(slot, total);
                                            again = false;
                                        } else {
                                            if (p1.slotHasPiece(newslot)) {
                                                Console.WriteLine("Jogador " + round + ": Já existe uma peça na posição futura que escolheu.");
                                            } else {
                                                p1.setPieceNewSlot(slot, total);
                                                again = false;
                                            }
                                        }
                                    } else {
                                        if (p1.slotHasPiece(newslot)) {
                                            Console.WriteLine("Jogador " + round + ": Já existe uma peça na posição futura que escolheu.");
                                        } else {
                                            p1.setPieceNewSlot(slot, total);
                                            again = false;
                                        }
                                    }
                                }
                            }
                        } else {
                            if (p2.slotHasPiece(slot) && (slot + total <= 15)) {
                                int newslot = slot + total;
                                if (newslot == 8 && p1.slotHasPiece(8)) {
                                    Console.WriteLine("Este lugar está ocupado.");
                                } else {
                                    if (newslot > 4 && newslot < 8 || newslot > 8 && newslot < 13) {
                                        if (p1.slotHasPiece(newslot)) {
                                            p1.setPieceNewSlot(newslot, -newslot);
                                            p2.setPieceNewSlot(slot, total);
                                                again = false;
                                        } else {
                                            if (p2.slotHasPiece(newslot)) {
                                                Console.WriteLine("Jogador " + round + ": Já existe uma peça na posição futura que escolheu.");
                                            } else {
                                                p2.setPieceNewSlot(slot, total);
                                                again = false;
                                            }
                                        }
                                    } else {
                                        if (p2.slotHasPiece(newslot)) {
                                            Console.WriteLine("Jogador " + round + ": Já existe uma peça na posição futura que escolheu.");
                                        } else {
                                            p2.setPieceNewSlot(slot, total);
                                            again = false;
                                        }
                                    }
                                }
                            }
                        }
                        Console.WriteLine("> Pressione qualquer tecla para continuar.");
                        Console.ReadLine();
                        if (again == false) break;
                    } 
                }
            } while(play);
        }

        /*Rolar os dados, onde "o" = 1 casa para andar e "\\" é igual a 0
          Exemplo: o\\\ = 1
                   \\\\ = 0
                   oooo = 4
        */
        static string numbersToChars(int numb) {
            if (numb == 1) return "o";
            if (numb == 2) return "\\";
            return "/";
        }

        //Localiza cada parte do mapa em números e coloca as peças de cada jogador onde devem estar
        //Criação do mapa exemplificado no ínicio
        static void showMap(Player player1, Player player2) {
            string a1 = "-", a2 = "-", b1 = "-", b2 = "-", c1 = "-", c2 = "-";
            string d1 = "*", d2 = "*";
            string e = "-", f = "-", g = "-", i = "-", j = "-", k = "-", l = "-";
            string h = "*";
            string n1 = "*", n2 = "*";
            string m1 = "-", m2 = "-";
            if (player1.slotHasPiece(1)) a1 = "1";
            if (player1.slotHasPiece(2)) b1 = "1";
            if (player1.slotHasPiece(3)) c1 = "1";
            if (player1.slotHasPiece(4)) d1 = "1";
            if (player1.slotHasPiece(5)) e = "1";
            if (player1.slotHasPiece(6)) f = "1";
            if (player1.slotHasPiece(7)) g = "1";
            if (player1.slotHasPiece(8)) h = "1";
            if (player1.slotHasPiece(9)) i = "1";
            if (player1.slotHasPiece(10)) j = "1";
            if (player1.slotHasPiece(11)) k = "1";
            if (player1.slotHasPiece(12)) l = "1";
            if (player1.slotHasPiece(13)) m1 = "1";
            if (player1.slotHasPiece(14)) n1 = "1";
            // /////////
            if (player2.slotHasPiece(1)) a2 = "2";
            if (player2.slotHasPiece(2)) b2 = "2";
            if (player2.slotHasPiece(3)) c2 = "2";
            if (player2.slotHasPiece(4)) d2 = "2";
            if (player2.slotHasPiece(5)) e = "2";
            if (player2.slotHasPiece(6)) f = "2";
            if (player2.slotHasPiece(7)) g = "2";
            if (player2.slotHasPiece(8)) h = "2";
            if (player2.slotHasPiece(9)) i = "2";
            if (player2.slotHasPiece(10)) j = "2";
            if (player2.slotHasPiece(11)) k = "2";
            if (player2.slotHasPiece(12)) l = "2";
            if (player2.slotHasPiece(13)) m2 = "2";
            if (player2.slotHasPiece(14)) n2 = "2";
            Console.WriteLine(d1 + e + d2);
            Console.WriteLine(c1 + f + c2);
            Console.WriteLine(b1 + g + b2);
            Console.WriteLine(a1 + h + a2);
            Console.WriteLine(" " + i + " ");
            Console.WriteLine(" " + j + " ");
            Console.WriteLine(n1 + k + n2);
            Console.WriteLine(m1 + l + m2);
        }
    }
}