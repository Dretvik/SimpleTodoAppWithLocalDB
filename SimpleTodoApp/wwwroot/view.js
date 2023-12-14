getData();
function updateView() {
    document.getElementById('app').innerHTML = /*HTML*/`
                <ul>
                    ${createTodoItemsHtml()}
                </ul>
                <input
                    type="text"
                    oninput="model.inputs.text=this.value"
                    value="${model.inputs.text || ''}"
                />
                <button onclick="createTodoItem()">Lag ny</button>
            `;
}

function createTodoItemsHtml() {
    return model.todoItems.map(todoItem => /*HTML*/`
                <li>
                    ${todoItem.text}
                    ${todoItem.done == null
            ? `<button onclick="registerDone('${todoItem.id}')">registrer gjort</button>`
            : `gjort ${new Date(todoItem.done).toLocaleDateString()}`}
                    <button onclick="deleteTodoItem('${todoItem.id}')">x</button>
                </li >
                `).join('');
}