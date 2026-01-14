# Ayllu 

## Fluxo REAL de uso da API (do ponto de vista do usuário)

Vamos percorrer exatamente suas rotas.

- 1. Criar uma Dialética

``` POST /api/v1/dialectics ```


✔️ Cria o aggregate
✔️ Estado inicial: Draft
✔️ Owner = usuário autenticado

📌 Sem isso nada existe

- 2. Publicar a Tese (inicia o debate)

``` POST /api/v1/dialectics/{dialecticId}/thesis ```


✔️ Cria a tese
✔️ Move estado → ThesisPublished
✔️ Só o dono pode fazer

📌 Aqui a dialética passa a existir publicamente

- 3. Abrir para Antíteses

(opcional se você não fizer automático no POST da tese)

``` PUT /api/v1/dialectics/{dialecticId}/thesis ```


ou internamente:

dialectic.OpenAntithesis();


✔️ Estado → AntithesisOpen

- 4. Outros usuários contribuem com Antíteses

``` POST /api/v1/dialectics/{dialecticId}/antitheses ```


✔️ Usuários (amigos / grupo / público) participam
✔️ Valida permissões
✔️ Valida estado = AntithesisOpen

- 5. Consultar Antíteses

``` GET /api/v1/dialectics/{dialecticId}/antitheses ```


✔️ Leitura pública ou restrita (regra sua)

- 6. Criar uma Síntese

``` POST /api/v1/dialectics/{dialecticId}/syntheses ```


✔️ Geralmente pelo dono ou admin do grupo
✔️ Usa tese + antíteses
✔️ Estado → SynthesisCreated

- 7. Consultar Sínteses

``` GET /api/v1/dialectics/{dialecticId}/syntheses ```

- 8. Fechar a Dialética 

``` POST /api/v1/dialectics/{id}/close ```


✔️ Estado → Closed
✔️ Nenhuma nova interação possível

- 9. Consultas transversais

Minhas Dialéticas

``` GET /api/v1/dialectics/mine ```


✔️ Owner = usuário autenticado

Dialéticas dos amigos

``` GET /api/v1/dialectics/friends ```


✔️ Usa ApplicationUserFriendship
✔️ Leitura somente

Buscar uma Dialética específica
GET /api/v1/dialectics/{id}


✔️ Mostra estado + relações