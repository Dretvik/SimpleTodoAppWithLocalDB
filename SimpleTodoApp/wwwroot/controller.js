async function getData() {
    const response = await axios.get('/todo');
    model.todoItems = response.data;
    updateView();
}

async function createTodoItem() {
    const todoItem = {
        text: model.inputs.text
    };
    const response = await axios.post('/todo', todoItem);
    await getData();
}

async function registerDone(id) {
    const response = await axios.put('/todo/' + id);
    await getData();
}

async function deleteTodoItem(id) {
    const response = await axios.delete('/todo/' + id);
    await getData();
}